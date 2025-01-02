using ecms.Application.Models.ViewModels.Materials;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Material.GetMaterialsByFilters;

public class GetMaterialsByFiltersQuery : IRequest<Result<FilteredMaterialsViewModel>>
{
    public string Name { get; set; } = string.Empty;

    public string BatchNumber { get; set; } = string.Empty;

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public bool OnlyActive { get; set; }
}
