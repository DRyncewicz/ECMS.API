using ecms.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteStock;

public class DeleteStockCommandHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<DeleteStockCommand, Result<string>>
{
    private const string result = "Cannot delete stock because there are products in. To delete stock, create internal transfer first";
    private const string empty = "";
    public async Task<Result<string>> Handle(DeleteStockCommand request, CancellationToken ct)
    {
        var stockToDelete = _applicationDbContext.Stocks.Include(p => p.StockLevels).FirstOrDefault(p => p.Id == request.StockId);

        if (stockToDelete != null)
        {
            if (stockToDelete.StockLevels.Any(p => p.Quantity > 0))
            {
                return Result.Success(result);
            }
            else
            {
                stockToDelete.IsDeleted = true;
                return Result.Success(empty);
            }
        }

        Result.Failure(new Error(nameof(NullReferenceException), $"Stock with {request.StockId} not found", ErrorType.Failure));
        throw new Exception();
    }
}
