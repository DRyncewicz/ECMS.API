using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.ViewModels.Materials;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetMaterialDetailsById;

public class GetMaterialDetailsByIdQueryHandler(IApplicationDbContext _applicationDbContext,
                                                IMapper _mapper) : IRequestHandler<GetMaterialDetailsByIdQuery, Result<MaterialDetailsViewModel>>
{
    public async Task<Result<MaterialDetailsViewModel>> Handle(GetMaterialDetailsByIdQuery request, CancellationToken ct)
    {
        var material = _applicationDbContext.Materials.Include(p => p.StockLevel).FirstOrDefault(p => p.Id == request.MaterialId);
        Ensure.NotNull(material);        
        var model = _mapper.Map<MaterialDetailsViewModel>(material);
        return Result.Success(model);
    }
}
