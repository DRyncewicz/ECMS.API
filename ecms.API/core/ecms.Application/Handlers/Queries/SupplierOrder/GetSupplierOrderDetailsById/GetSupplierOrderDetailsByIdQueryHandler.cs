using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.ViewModels.SupplierOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.SupplierOrder.GetSupplierOrderDetailsById;

public class GetSupplierOrderDetailsByIdQueryHandler(IApplicationDbContext _applicationDbContext,
                                                     IMapper _mapper) : IRequestHandler<GetSupplierOrderDetailsByIdQuery, Result<SupplierOrderDetailsViewModel>>
{
    public async Task<Result<SupplierOrderDetailsViewModel>> Handle(GetSupplierOrderDetailsByIdQuery request, CancellationToken ct)
    {
        var supplierOrder = await _applicationDbContext.SupplierOrders.AsNoTracking()
                                                                .AsSplitQuery()
                                                                .Include(p => p.SupplierOrderMaterials)
                                                                .ThenInclude(p => p.Material)
                                                                .ThenInclude(p => p.StockLevel)
                                                                .Include(p => p.Supplier)
                                                                .Include(p => p.SupplierContact)
                                                                .FirstOrDefaultAsync(p => p.Id == request.SupplierOrderId, ct);

        if (supplierOrder == null)
        {
            return Result.Failure<SupplierOrderDetailsViewModel>(Error.NotFound("404", $"There is no record with ID {request.SupplierOrderId}"));
        }

        var model = _mapper.Map<SupplierOrderDetailsViewModel>(supplierOrder);
        return Result.Success(model);
    }
}