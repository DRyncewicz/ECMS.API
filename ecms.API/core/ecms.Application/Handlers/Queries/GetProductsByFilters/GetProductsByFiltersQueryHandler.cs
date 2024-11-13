using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.Products;
using ecms.Application.Models.ViewModels.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetProductsByFilters;

/// <summary>
/// Gets products by filters
/// </summary>
/// <param name="_applicationDbContext"></param>
/// <param name="_mapper"></param>
public class GetProductsByFiltersQueryHandler(IApplicationDbContext _applicationDbContext,
                                              IMapper _mapper) : IRequestHandler<GetProductsByFiltersQuery, Result<FilteredProductsViewModel>>
{
    public async Task<Result<FilteredProductsViewModel>> Handle(GetProductsByFiltersQuery request, CancellationToken ct)
    {
        var products = _applicationDbContext.Products.Include(p => p.ProductVariants).Where(p => p.IsDeleted != true).AsQueryable();
        var model = new FilteredProductsViewModel();

        if (!string.IsNullOrEmpty(request.Name))
        {
            products = products.Where(p => p.Name.Contains(request.Name));
        }

        if (request.CategoryId > 0)
        {
            products = products.Where(p => p.CategoryId == request.CategoryId);
        }

        model.TotalCount = products.Count();

        if (request.CurrentPage > 0 && request.PageSize > 0)
        {
            products = products.Skip(request.CurrentPage * request.PageSize - request.PageSize)
                    .Take(request.PageSize);
        }

        model.Products = _mapper.Map<IEnumerable<ProductDto>>(products.ToList());

        return Result.Success(model);
    }
}