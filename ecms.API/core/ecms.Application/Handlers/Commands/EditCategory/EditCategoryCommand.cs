using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.EditCategory;

public class EditCategoryCommand : IRequest<Result<int>>
{
    public int CategoryId { get; set; }

    public HierarchyId AncestorHierarchyId { get; set; } = new HierarchyId();

    public string Name { get; set; } = string.Empty;

    public Guid? FileGuid { get; set; }

    public EditCategoryCommand(EditCategoryRequest request, int id)
    {
        CategoryId = id;
        AncestorHierarchyId = request.AncestorHierarchyId;
        Name = request.Name;
        FileGuid = request.FileGuid;
    }

    public EditCategoryCommand()
    {
    }
}