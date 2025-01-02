using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.Supplier.CreateSupplier;

public class CreateSupplierCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = string.Empty;

    public int AddressId { get; set; }
}
