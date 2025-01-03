using ecms.Application.Models.ViewModels.Categories;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Category.GetAllCategoriesPaged;

public class GetAllCategoriesPagedQuery : IRequest<Result<PagedCategoryViewModel>>
{
    public int CurrentPage { get; set; }

    public int PageSize { get; set; }
}