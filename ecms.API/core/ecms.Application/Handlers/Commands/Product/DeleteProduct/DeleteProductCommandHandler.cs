using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.Product.DeleteProduct;

public class DeleteProductCommandHandler(IApplicationDbContext _applicationDbContext,
                                         IMapper _mapper,
                                         ICurrentUserService _userService,
                                         IDateTimeProvider _dateTimeProvider) : IRequestHandler<DeleteProductCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync(ct);
        try
        {
            var product = _applicationDbContext.Products.FirstOrDefault(p => p.Id == request.ProductId);
            Ensure.NotNull(product);
            product.IsDeleted = true;
            _applicationDbContext.Products.Update(product);
            await _applicationDbContext.SaveChangesAsync(ct);

            var productHistory = _mapper.Map<ProductHistoryEntity>(product);
            productHistory.IsDeleted = true;
            productHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            productHistory.CreatorUserId = _userService.UserId;
            await _applicationDbContext.ProductHistories.AddAsync(productHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var productVariants = _applicationDbContext.ProductVariants.Where(p => p.ProductId == request.ProductId).ToList();
            productVariants.ForEach(p => p.IsDeleted = true);
            _applicationDbContext.ProductVariants.UpdateRange(productVariants);
            var productVariantHistory = _mapper.Map<List<ProductVariantHistoryEntity>>(productVariants);
            productVariantHistory.ForEach(p => { p.IsDeleted = true; p.CreateDateTimeUtc = _dateTimeProvider.UtcNow; p.CreatorUserId = _userService.UserId; });
            await _applicationDbContext.ProductVariantHistories.AddRangeAsync(productVariantHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            transaction.Commit();
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}