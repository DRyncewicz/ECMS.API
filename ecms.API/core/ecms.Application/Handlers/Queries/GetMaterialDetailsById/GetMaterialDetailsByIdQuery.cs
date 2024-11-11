using ecms.Application.Models.ViewModels.Materials;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetMaterialDetailsById;

public class GetMaterialDetailsByIdQuery : IRequest<Result<MaterialDetailsViewModel>>
{
    public int MaterialId { get; set; }

    public GetMaterialDetailsByIdQuery(int materialId)
    {
        MaterialId = materialId;
    }

}
