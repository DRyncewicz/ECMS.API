using ecms.Application.Models.Dtos.Materials;

namespace ecms.Application.Models.ViewModels.Materials;

public class FilteredMaterialsViewModel
{
    public IEnumerable<MaterialDto> Materials { get; set; } = [];

    public int TotalCount { get; set; }
}
