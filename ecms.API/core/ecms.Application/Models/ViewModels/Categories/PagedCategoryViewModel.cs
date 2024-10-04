using ecms.Application.Models.Dtos.Categories;

namespace ecms.Application.Models.ViewModels.Categories;

public class PagedCategoryViewModel
{
    public List<CategoryDto> Categories { get; set; } = [];

    public int TotalCount { get; set; }
}
