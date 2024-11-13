using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.Materials;
using ecms.Application.Models.ViewModels.Materials;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetMaterialsByFilters;

public class GetMaterialsByFiltersQueryHandler(IApplicationDbContext _applicationDbContext,
                                               IMapper _mapper) : IRequestHandler<GetMaterialsByFiltersQuery, Result<FilteredMaterialsViewModel>>
{
    public async Task<Result<FilteredMaterialsViewModel>> Handle(GetMaterialsByFiltersQuery request, CancellationToken ct)
    {
        var materials = _applicationDbContext.Materials.Include(p => p.StockLevel).Where(p => p.IsDeleted == false).AsNoTracking();    
        var model = new FilteredMaterialsViewModel();

        if (!string.IsNullOrEmpty(request.Name))
        {
            materials = materials.Where(p => p.Name.Contains(request.Name));
        }

        if (!string.IsNullOrEmpty(request.BatchNumber))
        {
            materials = materials.Where(p => p.StockLevel.BatchNumber.Contains(request.BatchNumber));
        }

        if (request.OnlyActive == true)
        {
            materials = materials.Where(p => p.IsActive == true);
        }

        model.TotalCount = materials.Count();

        if (request.CurrentPage > 0 && request.PageSize > 0)
        {
            materials = materials.Skip(request.CurrentPage * request.PageSize - request.PageSize)
                                 .Take(request.PageSize);
        }

        var materialsDto = new List<MaterialDto>();
        foreach (var material in materials)
        {
            var materialDto = _mapper.Map<MaterialDto>(material);
            materialDto.StockId = material.StockLevel.StockId;
            materialsDto.Add(materialDto);
        }

        model.Materials = materialsDto;

        return Result.Success(model);
    }
}
