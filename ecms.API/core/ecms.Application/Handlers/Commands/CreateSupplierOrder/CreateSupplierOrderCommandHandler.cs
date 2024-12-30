using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateSupplierOrder;

public class CreateSupplierOrderCommandHandler(IApplicationDbContext _applicationDbContext,
                                               IMapper _mapper) : IRequestHandler<CreateSupplierOrderCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateSupplierOrderCommand request, CancellationToken ct)
    {
        var supplierOrder = _mapper.Map<SupplierOrderEntity>(request);
        supplierOrder.Status = Domain.Enums.StatusType.Pending;
        await _applicationDbContext.SupplierOrders.AddAsync(supplierOrder, ct);
        await _applicationDbContext.SaveChangesAsync(ct);

        var supplierOrderMaterials = _mapper.Map<List<SupplierOrderMaterialEntity>>(request.SupplierOrderMaterialDtos);
        supplierOrderMaterials.ForEach(p => p.SupplierOrderId = supplierOrder.Id);
        await _applicationDbContext.SupplierOrderMaterials.AddRangeAsync(supplierOrderMaterials, ct);
        return await _applicationDbContext.SaveChangesAsync(ct);
    }
}