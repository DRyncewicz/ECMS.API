using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.Materials;
using ecms.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.LinkProductVariantMaterials;

public class LinkProductVariantMaterialsCommandHandler(IApplicationDbContext _applicationDbContext,
                                                       IMapper _mapper,
                                                       IDateTimeProvider _dateTimeProvider,
                                                       ICurrentUserService _userService) : IRequestHandler<LinkProductVariantMaterialsCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(LinkProductVariantMaterialsCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync();
        try
        {
            var existingProductMaterials = _applicationDbContext.ProductMaterials.AsNoTracking().Where(p => p.ProductVariantId == request.ProductVariantId).ToList();

            var productMaterialsToAddDtos = request.ProductMaterialDtos.Where(p => !existingProductMaterials.Select(p => p.MaterialId).Contains(p.MaterialId)).ToList();
            await AddProductMaterialsAsync(productMaterialsToAddDtos, request, ct);

            var productMaterialsToEdit = existingProductMaterials.Where(p => request.ProductMaterialDtos.Select(p => p.MaterialId).Contains(p.MaterialId)).ToList();
            await EditProductMaterialsAsync(productMaterialsToEdit, request, ct);

            var productMaterialsToRemoveEntities = existingProductMaterials.Where(p => !request.ProductMaterialDtos.Select(p => p.MaterialId).Contains(p.MaterialId)).ToList();
            await DeleteProductMaterialsAsync(productMaterialsToRemoveEntities, ct);

            transaction.Commit();
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task DeleteProductMaterialsAsync(List<ProductMaterialEntity> productMaterialsToDelete, CancellationToken ct)
    {
        if (productMaterialsToDelete.Any())
        {
            productMaterialsToDelete.ForEach(p => p.IsDeleted = true);
            _applicationDbContext.ProductMaterials.UpdateRange(productMaterialsToDelete);
            var productMaterialHistories = _mapper.Map<List<ProductMaterialHistoryEntity>>(productMaterialsToDelete);
            productMaterialHistories.ForEach(p => { p.CreateDateTimeUtc = _dateTimeProvider.UtcNow; p.CreatorUserId = _userService.UserId; });
            await _applicationDbContext.ProductMaterialHistories.AddRangeAsync(productMaterialHistories, ct);
            await _applicationDbContext.SaveChangesAsync(ct);
        }
    }

    private async Task EditProductMaterialsAsync(List<ProductMaterialEntity> productMaterialsToEdit, LinkProductVariantMaterialsCommand request, CancellationToken ct)
    {
        if (productMaterialsToEdit.Any())
        {
            productMaterialsToEdit.ForEach(p => p.Quantity = request.ProductMaterialDtos.First(p => p.MaterialId == p.MaterialId).Quantity);
            _applicationDbContext.ProductMaterials.UpdateRange(productMaterialsToEdit);
            var productMaterialHistories = _mapper.Map<List<ProductMaterialHistoryEntity>>(productMaterialsToEdit);
            productMaterialHistories.ForEach(p => { p.CreateDateTimeUtc = _dateTimeProvider.UtcNow; p.CreatorUserId = _userService.UserId; });
            await _applicationDbContext.ProductMaterialHistories.AddRangeAsync(productMaterialHistories, ct);
            await _applicationDbContext.SaveChangesAsync(ct);
        }
    }

    private async Task AddProductMaterialsAsync(List<ProductMaterialDto> productMaterialsToAddDtos, LinkProductVariantMaterialsCommand request, CancellationToken ct)
    {
        if (productMaterialsToAddDtos.Any())
        {
            var productMaterials = new List<ProductMaterialEntity>();
            foreach (var productMaterialDto in productMaterialsToAddDtos)
            {
                var productMaterialToAddEntity = _mapper.Map<ProductMaterialEntity>(productMaterialDto);
                productMaterialToAddEntity.ProductVariantId = request.ProductVariantId;
                productMaterials.Add(productMaterialToAddEntity);
            }
            _applicationDbContext.ProductMaterials.AddRange(productMaterials);
            await _applicationDbContext.SaveChangesAsync(ct);

            var productMaterialHistories = new List<ProductMaterialHistoryEntity>();
            foreach (var productMaterial in productMaterials)
            {
                var productMaterialHistory = _mapper.Map<ProductMaterialHistoryEntity>(productMaterial);
                productMaterialHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
                productMaterialHistory.CreatorUserId = _userService.UserId;
                productMaterialHistories.Add(productMaterialHistory);
            }
            await _applicationDbContext.ProductMaterialHistories.AddRangeAsync(productMaterialHistories, ct);
            await _applicationDbContext.SaveChangesAsync(ct);
        }
    }
}