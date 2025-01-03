using ecms.Application.Models.ViewModels.Suppliers;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Supplier.GetSupplierDetailsById;

public class GetSupplierDetailsByIdQuery : IRequest<Result<SupplierDetailsViewModel>>
{
    public int SupplierId { get; set; }

    public GetSupplierDetailsByIdQuery(int supplierId)
    {
        SupplierId = supplierId;
    }
}
