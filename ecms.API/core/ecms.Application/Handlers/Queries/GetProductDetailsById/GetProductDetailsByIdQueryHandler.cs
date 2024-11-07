using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.Allergens;
using ecms.Application.Models.Dtos.Products;
using ecms.Application.Models.ViewModels.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetProductDetailsById;

public class GetProductDetailsByIdQueryHandler(IApplicationDbContext _applicationDbContext,
                                               IMapper _mapper) : IRequestHandler<GetProductDetailsByIdQuery, Result<ProductDetailsViewModel>>
{
    public async Task<Result<ProductDetailsViewModel>> Handle(GetProductDetailsByIdQuery request, CancellationToken ct)
    {
        var product = _applicationDbContext.Products.Include(p => p.ProductVariants)
                                                    .ThenInclude(p => p.ProductVariantAllergens)
                                                    .ThenInclude(p => p.Allergen)
                                                    .AsSplitQuery()
                                                    .FirstOrDefault(p => p.Id == request.ProductId);

        var productVariantDtos = new List<ProductVariantDto>();

        Ensure.NotNull(product);

        foreach (var productVariant in product.ProductVariants)
        {
            var allergens = productVariant.ProductVariantAllergens.Select(p => p.Allergen);
            var productVariantDto = _mapper.Map<ProductVariantDto>(productVariant);
            productVariantDto.Allergens = _mapper.Map<List<AllergenDto>>(allergens);
            productVariantDtos.Add(productVariantDto);
        }

        var model = new ProductDetailsViewModel()
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId,
            productVariantDtos = productVariantDtos,
            Vat = product.Vat,
            FileGuid = product.FileGuid,
            Unit = product.Unit,
            AlcoholContent = product.AlcoholContent,
            GtuCode = product.GtuCode,
        };

        return Result.Success(model);
    }
}