using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using FluentValidation.Validators;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateProduct;

public class CreateProductCommandHandler(IApplicationDbContext _applicationDbContext,
                                         IMapper _mapper,
                                         ICurrentUserService _userService,
                                         IDateTimeProvider _dateTimeProvider) : IRequestHandler<CreateProductCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync();
        try
        {
            var productEntity = _mapper.Map<ProductEntity>(request);
            productEntity.UserId = _userService.UserId;
            await _applicationDbContext.Products.AddAsync(productEntity, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var productHistory = _mapper.Map<ProductHistoryEntity>(productEntity);           
            productHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            productHistory.CreatorUserId = _userService.UserId;
            await _applicationDbContext.ProductHistories.AddAsync(productHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var productVariants = _mapper.Map<List<ProductVariantEntity>>(request.ProductVariants);
            productVariants.ForEach(p => p.ProductId = productEntity.Id);
            await _applicationDbContext.ProductVariants.AddRangeAsync(productVariants);
            await _applicationDbContext.SaveChangesAsync(ct);            

            var productVariantHistory = _mapper.Map<List<ProductVariantHistoryEntity>>(productVariants);
            productVariantHistory.ForEach(p => { p.CreatorUserId = _userService.UserId; p.CreateDateTimeUtc = _dateTimeProvider.UtcNow; });
            await _applicationDbContext.ProductVariantHistories.AddRangeAsync(productVariantHistory);
            await _applicationDbContext.SaveChangesAsync(ct);

            transaction.Commit();
            return Result.Success(productEntity.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }

    }

}

