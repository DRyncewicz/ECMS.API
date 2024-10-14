using Microsoft.EntityFrameworkCore;

namespace ecms.Application.Handlers.Commands.EditCategory;

public class EditCategoryRequest
{
    public HierarchyId AncestorHierarchyId { get; set; } = new HierarchyId();

    public string Name { get; set; } = string.Empty;
    public Guid? FileGuid { get; set; }
}