using Microsoft.EntityFrameworkCore;

namespace ecms.Application.Models.Dtos.Categories;

public class CategoryDto
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public HierarchyId HierarchyId { get; set; } = new();

    public string AncestorName { get; set; } = string.Empty;
}
