using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.EditProduct;

public class EditProductCommandHandler(IApplicationDbContext _applicationDbContext,
                                       IMapper _mapper,
                                       ICurrentUserService _userService,
                                       IDateTimeProvider _dateTimeProvider) : IRequestHandler<EditProductCommand, Result<int>>
{
    public async Task<Result<int>> Handle(EditProductCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync();
        try
        {
            var productToEdit = _mapper.Map<ProductEntity>(request);
            _applicationDbContext.Products.Update(productToEdit);
            await _applicationDbContext.SaveChangesAsync(ct);

            var productHistory = _mapper.Map<ProductHistoryEntity>(productToEdit);
            productHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            productHistory.CreatorUserId = _userService.UserId;
            await _applicationDbContext.ProductHistories.AddAsync(productHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var oldProductVariants = _applicationDbContext.ProductVariants.Where(p => p.ProductId == request.Id && p.IsDeleted == false).AsNoTracking().ToList();

            if (request.ProductVariants.Select(variant => variant.Id).Any(x => x == 0))
            {
                var productVariants = request.ProductVariants.Where(p => p.Id == 0);
                var newProductVariants = _mapper.Map<List<ProductVariantEntity>>(productVariants);
                await _applicationDbContext.ProductVariants.AddRangeAsync(newProductVariants);
                await _applicationDbContext.SaveChangesAsync(ct);

                var productVariantHistory = _mapper.Map<List<ProductVariantHistoryEntity>>(newProductVariants);
                productVariantHistory.ForEach(p => { p.CreatorUserId = _userService.UserId; p.CreateDateTimeUtc = _dateTimeProvider.UtcNow; });
                await _applicationDbContext.ProductVariantHistories.AddRangeAsync(productVariantHistory);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            var oldProductVariantsIds = oldProductVariants.Select(p => p.Id);

            if (oldProductVariantsIds.Any(p => request.ProductVariants.Select(p => p.Id).Contains(p)))
            {
                var productVariantsToUpdate = new List<ProductVariantEntity>();
                foreach (var productVariant in request.ProductVariants)
                {
                    if (productVariant.Id != 0 && oldProductVariantsIds.Contains(productVariant.Id))
                    {
                        var productVariantToEdit = _mapper.Map<ProductVariantEntity>(productVariant);

                        if (!oldProductVariants.Contains(productVariantToEdit))
                        {
                            productVariantsToUpdate.Add(productVariantToEdit);
                        }
                    }
                }
                _applicationDbContext.ProductVariants.UpdateRange(productVariantsToUpdate);
                await _applicationDbContext.SaveChangesAsync(ct);

                var productVariantHistory = _mapper.Map<List<ProductVariantHistoryEntity>>(productVariantsToUpdate);
                productVariantHistory.ForEach(p => { p.CreatorUserId = _userService.UserId; p.CreateDateTimeUtc = _dateTimeProvider.UtcNow; });
                await _applicationDbContext.ProductVariantHistories.AddRangeAsync(productVariantHistory);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            if (oldProductVariantsIds.Any(p => !request.ProductVariants.Select(p => p.Id).Contains(p)))
            {
                var productVariantsToDelete = new List<ProductVariantEntity>();
                foreach (var oldProductVariant in oldProductVariants)
                {
                    if (!request.ProductVariants.Select(p => p.Id).Contains(oldProductVariant.Id))
                    {
                        oldProductVariant.IsDeleted = true;
                        productVariantsToDelete.Add(oldProductVariant);
                    }
                }
                _applicationDbContext.ProductVariants.UpdateRange(productVariantsToDelete);
                await _applicationDbContext.SaveChangesAsync(ct);

                var productVariantHistory = _mapper.Map<List<ProductVariantHistoryEntity>>(productVariantsToDelete);
                productVariantHistory.ForEach(p => { p.CreatorUserId = _userService.UserId; p.CreateDateTimeUtc = _dateTimeProvider.UtcNow; });
                await _applicationDbContext.ProductVariantHistories.AddRangeAsync(productVariantHistory);
                await _applicationDbContext.SaveChangesAsync(ct);
            }
            transaction.Commit();
            return Result.Success(productToEdit.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}