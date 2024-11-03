using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteStock;

public class DeleteStockCommand : IRequest<Result<string>>
{
    public int StockId { get; set; }

    public DeleteStockCommand(int stockId)
    {
        StockId = stockId;
    }
}