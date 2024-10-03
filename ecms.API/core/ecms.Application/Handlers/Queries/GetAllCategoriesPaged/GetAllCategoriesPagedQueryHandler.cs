using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.Categories;
using ecms.Application.Models.ViewModels.Categories;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetAllCategoriesPaged;

public class GetAllCategoriesPagedQueryHandler(IApplicationDbContext _applicationDbContext,
                                               IMapper _mapper) : IRequestHandler<GetAllCategoriesPagedQuery, Result<PagedCategoryViewModel>>
{
    public async Task<Result<PagedCategoryViewModel>> Handle(GetAllCategoriesPagedQuery request, CancellationToken ct)
    {
        var categories = _applicationDbContext.Categories.ToList();
        var model = new PagedCategoryViewModel();

        model.TotalCount = categories.Count();

        if (request.CurrentPage > 0 && request.PageSize > 0)
        {
            categories = categories.Skip(request.CurrentPage * request.PageSize - request.PageSize)
                    .Take(request.PageSize).ToList();
        }

        model.Categories = _mapper.Map<List<CategoryDto>>(categories.ToList());

        //var categoryDtos = _mapper.Map<List<CategoryDto>>(categories.ToList());
        //foreach (var categoryDto in categoryDtos)
        //{
        //    categoryDto.AncestorName = categories.FirstOrDefault(p => )
        //}
        //model.Categories = categoryDtos;

        return Result.Success(model);
    }
}