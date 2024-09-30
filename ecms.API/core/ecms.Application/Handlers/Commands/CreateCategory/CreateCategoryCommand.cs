using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Result<int>>
{
    public HierarchyId AncenstorHierarchyId { get; set; } = new HierarchyId();

    public string Name { get; set; } = string.Empty;

    public Guid? FileGuid { get; set; }
}
