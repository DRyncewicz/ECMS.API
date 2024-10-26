using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateStock;

public class CreateStockCommand : IRequest<Result<int>>
{
    public string Name { get; set; }

    public int AddressId { get; set; }

    public string Description { get; set; }
}
