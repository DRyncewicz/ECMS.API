using ecms.Application.Models.ViewModels.Stocks;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Stock.GetAllStocksWithAddresses;

public class GetAllStocksWithAddressesQuery : IRequest<Result<StockViewModel>>
{
}