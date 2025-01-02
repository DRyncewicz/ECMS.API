using ecms.Application.Models.ViewModels.Products;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Product.GetProductsByFilters;

public class GetProductsByFiltersQuery : IRequest<Result<FilteredProductsViewModel>>
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }
}