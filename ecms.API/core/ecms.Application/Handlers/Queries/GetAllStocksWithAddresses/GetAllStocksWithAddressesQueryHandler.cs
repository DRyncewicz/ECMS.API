using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.Stocks;
using ecms.Application.Models.ViewModels.Stocks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetAllStocksWithAddresses
{
    public class GetAllStocksWithAddressesQueryHandler(IApplicationDbContext _applicationDbContext,
                                                       IMapper _mapper) : IRequestHandler<GetAllStocksWithAddressesQuery, Result<StockViewModel>>
    {
        public async Task<Result<StockViewModel>> Handle(GetAllStocksWithAddressesQuery request, CancellationToken ct)
        {
            var stocks = _applicationDbContext.Stocks.AsNoTracking()
                                                     .Include(p => p.Address)
                                                     .Where(x => !x.IsDeleted)
                                                     .ToList();
            var model = new StockViewModel();

            var stockDtos = _mapper.Map<List<StockDto>>(stocks);

            model.Stocks = stockDtos;

            return Result.Success(model);
        }
    }
}