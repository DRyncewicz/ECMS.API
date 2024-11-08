using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteMaterial;

public class DeleteMaterialCommand : IRequest<Result<bool>>
{
    public int MaterialId { get; set; }

    public DeleteMaterialCommand(int materialId)
    {
        MaterialId = materialId;
    }
}