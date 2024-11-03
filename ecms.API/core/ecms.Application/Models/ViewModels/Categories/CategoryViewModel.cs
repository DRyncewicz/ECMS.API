using Microsoft.EntityFrameworkCore;

namespace ecms.Application.Models.ViewModels.Categories;

public class CategoryViewModel
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public HierarchyId HierarchyId { get; set; } = new HierarchyId();

    public string AncestorName { get; set; } = string.Empty;

    public Guid? FileGuid { get; set; }

    public HierarchyId AncestorHierarchyId { get; set; } = new HierarchyId();
}