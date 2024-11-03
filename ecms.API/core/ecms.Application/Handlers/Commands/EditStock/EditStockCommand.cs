using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.EditStock;

public class EditStockCommand : IRequest<Result<int>>
{
    public int StockId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int AddressId { get; set; }

    public EditStockCommand(EditStockRequest request, int id)
    {
        StockId = id;
        Name = request.Name;
        Description = request.Description;
        AddressId = request.AddressId;
    }

    public EditStockCommand()
    {
    }
}