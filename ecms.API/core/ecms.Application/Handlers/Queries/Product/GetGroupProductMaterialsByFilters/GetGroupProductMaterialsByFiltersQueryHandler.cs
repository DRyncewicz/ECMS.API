using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.Products;
using ecms.Application.Models.ViewModels.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Product.GetGroupProductMaterialsByFilters;

public class GetGroupProductMaterialsByFiltersQueryHandler(IApplicationDbContext _applicationDbContext,
                                                           IMapper _mapper) : IRequestHandler<GetGroupProductMaterialsByFiltersQuery, Result<GroupProductMaterialViewModel>>
{
    public async Task<Result<GroupProductMaterialViewModel>> Handle(GetGroupProductMaterialsByFiltersQuery request, CancellationToken ct)
    {
        var productMaterials = _applicationDbContext.ProductMaterials.Where(p => p.IsDeleted == false);

        if (request.ProductVariantIds.Any())
        {
            productMaterials = productMaterials.Where(p => request.ProductVariantIds.Contains(p.ProductVariantId));
        }

        if (request.MaterialIds.Any())
        {
            productMaterials = productMaterials.Where(p => request.MaterialIds.Contains(p.MaterialId));
        }

        productMaterials = productMaterials.Include(p => p.ProductVariant)
                                           .Include(p => p.Material);

        var groupedResult = await productMaterials
            .GroupBy(pm => pm.ProductVariantId)
            .Select(g => new ProductVariantMaterialGroupDto
            {
                ProductVariantId = g.Key,
                ProductVariantName = g.First().ProductVariant.Name,
                productMaterialListItems = g.OrderBy(pm => pm.Material.Name)
                                     .Select(pm => _mapper.Map<ProductMaterialListItemDto>(pm))
                                     .ToList()
            })
            .ToListAsync(ct);

        var model = new GroupProductMaterialViewModel
        {
            ProductVariantMaterialGroups = groupedResult
        };

        return Result.Success(model);
    }
}