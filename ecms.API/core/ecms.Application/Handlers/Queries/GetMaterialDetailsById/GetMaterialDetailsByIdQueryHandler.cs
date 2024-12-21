using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.ViewModels.Materials;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetMaterialDetailsById;

public class GetMaterialDetailsByIdQueryHandler(IApplicationDbContext _applicationDbContext,
                                                IMapper _mapper) : IRequestHandler<GetMaterialDetailsByIdQuery, Result<MaterialDetailsViewModel>>
{
    public async Task<Result<MaterialDetailsViewModel>> Handle(GetMaterialDetailsByIdQuery request, CancellationToken ct)
    {
        var material = _applicationDbContext.Materials.AsNoTracking()
                                                      .Include(p => p.StockLevel)
                                                      .FirstOrDefault(p => p.Id == request.MaterialId);

        if (material is null)
        {
            return Result.Failure<MaterialDetailsViewModel>(Error.NotFound("404", $"There is no record with ID {request.MaterialId}"));
        }

        var model = _mapper.Map<MaterialDetailsViewModel>(material);
        return Result.Success(model);
    }
}