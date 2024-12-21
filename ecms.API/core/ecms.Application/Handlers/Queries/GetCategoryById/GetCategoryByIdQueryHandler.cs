using ecms.Application.Abstractions.Data;
using ecms.Application.Models.ViewModels.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryViewModel>>
{
    public async Task<Result<CategoryViewModel>> Handle(GetCategoryByIdQuery request, CancellationToken ct)
    {
        var category = _applicationDbContext.Categories.AsNoTracking()
                                                       .FirstOrDefault(p => p.Id == request.CategoryId);

        if (category is null)
        {
            return Result.Failure<CategoryViewModel>(Error.NotFound("404", $"There is no record with ID {request.CategoryId}"));
        }

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