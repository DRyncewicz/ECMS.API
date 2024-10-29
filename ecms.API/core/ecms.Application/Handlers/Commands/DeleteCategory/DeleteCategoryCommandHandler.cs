using ecms.Application.Abstractions.Data;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    private const string result = "Unable to delete category because there are sub categories, delete or change parent categories first";
    private const string empty = "";

    public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken ct)
    {
        var categoryToDelete = _applicationDbContext.Categories.FirstOrDefault(p => p.Id == request.CategoryId);

        Ensure.NotNull(categoryToDelete);

        if (_applicationDbContext.Categories.Where(p => p.Id != categoryToDelete.Id).Any(p => p.HierarchyId.IsDescendantOf(categoryToDelete.HierarchyId)))
        {
            return Result.Success(result);
        }
        else
        {
            _applicationDbContext.Categories.Remove(categoryToDelete);
            await _applicationDbContext.SaveChangesAsync(ct);
            return Result.Success(empty);
        }
    }
}