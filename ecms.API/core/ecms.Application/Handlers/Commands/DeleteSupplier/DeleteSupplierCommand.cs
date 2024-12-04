using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteSupplier;

public class DeleteSupplierCommand : IRequest<Result<bool>>
{
    public int SupplierId { get; set; }

    public DeleteSupplierCommand(int supplierId)
    {
        SupplierId = supplierId;
    }

}
