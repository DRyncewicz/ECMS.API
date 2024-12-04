using ecms.Application.Models.ViewModels.Suppliers;
using MediatR;
using SharedKernel;

namespace UnitTests.Handlers.Queries.GetSuppliersPaged;

public class GetSuppliersPagedQuery : IRequest<Result<PagedSupplierViewModel>>
{
    public int CurrentPage { get; set; }

    public int PageSize { get; set; }
}