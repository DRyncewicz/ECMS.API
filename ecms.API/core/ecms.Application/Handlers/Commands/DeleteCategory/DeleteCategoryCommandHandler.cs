using ecms.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    private const string containsAnyDescendantsResponse = "Unable to delete category because there are sub categories, delete or change parent categories first";
    private const string containsAnyProductsResponse = "Unable to delete category because there are active products in that category, first delete the products or change their categories";

    public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken ct)
    {
        var categoryToDelete = _applicationDbContext.Categories.Include(x => x.Products).FirstOrDefault(p => p.Id == request.CategoryId);

        Ensure.NotNull(categoryToDelete);

        if (_applicationDbContext.Categories.Where(p => p.Id != categoryToDelete.Id).Any(p => p.HierarchyId.IsDescendantOf(categoryToDelete.HierarchyId)))
        {
            return Result.Success(containsAnyDescendantsResponse);
        }
        else if (categoryToDelete.Products.Any(x => x.IsDeleted == false))
        {
            return Result.Success(containsAnyProductsResponse);
        }
        else
        {
            _applicationDbContext.Categories.Remove(categoryToDelete);
            await _applicationDbContext.SaveChangesAsync(ct);
            return Result.Success(string.Empty);
        }
    }
}