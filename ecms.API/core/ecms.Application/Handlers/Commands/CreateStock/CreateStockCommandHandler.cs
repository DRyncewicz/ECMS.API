using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateStock;

public class CreateStockCommandHandler(IApplicationDbContext _applicationDbContext,
                                       IMapper _mapper) : IRequestHandler<CreateStockCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateStockCommand request, CancellationToken ct)
    {
        var stockEntity = _mapper.Map<StockEntity>(request);
        stockEntity.IsDeleted = false;
        
        await _applicationDbContext.Stocks.AddAsync(stockEntity, ct);
        await _applicationDbContext.SaveChangesAsync(ct);

        return Result.Success(stockEntity.Id);
    }
}
