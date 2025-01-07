using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Abstractions.Emails;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;

public class EditSupplierOrderCommandHandler(IApplicationDbContext _applicationDbContext,
                                             IMapper _mapper,
                                             IDateTimeProvider _dateTimeProvider,
                                             IEmailGenerator _emailGenerator) : IRequestHandler<EditSupplierOrderCommand, Result<int>>
{
    public async Task<Result<int>> Handle(EditSupplierOrderCommand request, CancellationToken ct)
    {
        var supplierOrderToEdit = _applicationDbContext.SupplierOrders.FirstOrDefault(p => p.Id == request.SupplierOrderId);
        _mapper.Map(request, supplierOrderToEdit);
        Ensure.NotNull(supplierOrderToEdit);
        await _applicationDbContext.SaveChangesAsync(ct);
        return supplierOrderToEdit.Id;
    }
}