using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.EditStock;

public class EditStockCommandHandler(IApplicationDbContext _applicationDbContext,
                                     IMapper _mapper) : IRequestHandler<EditStockCommand, Result<int>>
{
    public async Task<Result<int>> Handle(EditStockCommand request, CancellationToken ct)
    {
        var stockToEdit = _mapper.Map<StockEntity>(request);
        _applicationDbContext.Stocks.Update(stockToEdit);
        await _applicationDbContext.SaveChangesAsync(ct);

        return Result.Success(stockToEdit.Id);
    }
}