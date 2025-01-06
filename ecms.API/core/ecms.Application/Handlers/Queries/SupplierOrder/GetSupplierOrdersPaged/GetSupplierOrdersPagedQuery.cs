using ecms.Application.Models.ViewModels.SupplierOrders;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.SupplierOrder.GetSupplierOrdersPaged;

public class GetSupplierOrdersPagedQuery : IRequest<Result<SupplierOrdersViewModel>>
{
    public int PageSize { get; set; }

    public int CurrentPage { get; set; }
}