using AutoMapper;
using ecms.Application.Abstractions.Data;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.EditCategory;

public class EditCategoryCommandHandler(IApplicationDbContext _applicationDbContext,
                                        IMapper _mapper) : IRequestHandler<EditCategoryCommand, Result<int>>
{
    public async Task<Result<int>> Handle(EditCategoryCommand request, CancellationToken ct)
    {
        var categoryToEdit = _applicationDbContext.Categories.FirstOrDefault(p => p.Id == request.CategoryId);
        _mapper.Map(request, categoryToEdit);
        var existingAncestorHierarchyId = _applicationDbContext.Categories.FirstOrDefault(p => p.Id == request.CategoryId).HierarchyId.GetAncestor(1);

        if (existingAncestorHierarchyId != request.AncestorHierarchyId)
        {
            var children = _applicationDbContext.Categories.Where(p => p.HierarchyId.IsDescendantOf(request.AncestorHierarchyId) && p.HierarchyId.GetLevel() == request.AncestorHierarchyId.GetLevel() + 1);
            if (children.Any())
            {
                var highestChild = children.OrderBy(p => p.HierarchyId).Last();
                categoryToEdit.HierarchyId = request.AncestorHierarchyId.GetDescendant(highestChild.HierarchyId);
            }
            else
            {
                categoryToEdit.HierarchyId = request.AncestorHierarchyId.GetDescendant(null);
            }
        }

        _applicationDbContext.Categories.Update(categoryToEdit);
        await _applicationDbContext.SaveChangesAsync(ct);

        return Result.Success(categoryToEdit.Id);
    }
}