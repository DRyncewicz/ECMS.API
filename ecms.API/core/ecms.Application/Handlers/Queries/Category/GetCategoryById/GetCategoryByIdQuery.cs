using ecms.Application.Models.ViewModels.Categories;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Category.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<Result<CategoryViewModel>>
{
    public int CategoryId { get; set; }

    public GetCategoryByIdQuery(int categoryId)
    {
        CategoryId = categoryId;
    }
}