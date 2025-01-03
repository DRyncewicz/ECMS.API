using ecms.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.Stock.DeleteStock;

public class DeleteStockCommandHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<DeleteStockCommand, Result<string>>
{
    private const string result = "Cannot delete stock because there are products in. To delete stock, create internal transfer first";
    private const string empty = "";

    public async Task<Result<string>> Handle(DeleteStockCommand request, CancellationToken ct)
    {
        var stockToDelete = _applicationDbContext.Stocks.Include(p => p.StockLevels).FirstOrDefault(p => p.Id == request.StockId);

        Ensure.NotNull(stockToDelete);

        if (stockToDelete.StockLevels.Any(p => p.Quantity > 0))
        {
            return Result.Success(result);
        }
        else
        {
            stockToDelete.IsDeleted = true;
            _applicationDbContext.Stocks.Update(stockToDelete);
            await _applicationDbContext.SaveChangesAsync(ct);
            return Result.Success(empty);
        }
    }
}