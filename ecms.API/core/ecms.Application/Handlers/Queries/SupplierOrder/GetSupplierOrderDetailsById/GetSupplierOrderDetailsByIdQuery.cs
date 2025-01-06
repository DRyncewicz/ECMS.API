using ecms.Application.Models.ViewModels.SupplierOrders;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.SupplierOrder.GetSupplierOrderDetailsById;

public class GetSupplierOrderDetailsByIdQuery : IRequest<Result<SupplierOrderDetailsViewModel>>
{
    public int SupplierOrderId { get; set; }

    public GetSupplierOrderDetailsByIdQuery(int supplierOrderId)
    {
        SupplierOrderId = supplierOrderId;
    }
}