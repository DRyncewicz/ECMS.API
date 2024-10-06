using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.DeleteProduct;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    private const string result = "Unable to delete category because there are sub categories, delete or change parent categories first";
    private const string empty = "";
    public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken ct)
    {
        var categoryToDelete = _applicationDbContext.Categories.FirstOrDefault(p => p.Id == request.CategoryId);

        if (categoryToDelete != null)
        {
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

        Result.Failure(new Error(nameof(NullReferenceException), $"Category with {request.CategoryId} not found", ErrorType.Failure));
        throw new Exception();
    }
}
