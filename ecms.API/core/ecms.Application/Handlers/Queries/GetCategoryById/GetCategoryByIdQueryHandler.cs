using ecms.Application.Abstractions.Data;
using ecms.Application.Models.ViewModels.Categories;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryViewModel>>
{
    private const string result = "Category not found";
    public async Task<Result<CategoryViewModel>> Handle(GetCategoryByIdQuery request, CancellationToken ct)
    {
        var category = _applicationDbContext.Categories.FirstOrDefault(p => p.Id == request.CategoryId);

        Ensure.NotNull(category);
        var ancestorId = category.HierarchyId.GetAncestor(1);
        var model = new CategoryViewModel
        {
            CategoryId = category.Id,
            Name = category.Name,
            HierarchyId = category.HierarchyId,
            FileGuid = category.FileGuid,
        };

        if (ancestorId != null)
        {
            var ancestor = _applicationDbContext.Categories.FirstOrDefault(p => p.HierarchyId == ancestorId);
            model.AncestorName = ancestor.Name;
            model.AncestorHierarchyId = ancestor.HierarchyId;
        }
        return Result.Success(model);
    }
}
