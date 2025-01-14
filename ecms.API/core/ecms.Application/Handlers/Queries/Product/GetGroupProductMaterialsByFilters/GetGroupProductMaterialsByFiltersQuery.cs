using ecms.Application.Models.ViewModels.Products;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Product.GetGroupProductMaterialsByFilters;

public class GetGroupProductMaterialsByFiltersQuery : IRequest<Result<GroupProductMaterialViewModel>>
{
    public IEnumerable<int> ProductVariantIds { get; set; } = [];

    public IEnumerable<int> MaterialIds { get; set; } = [];
}