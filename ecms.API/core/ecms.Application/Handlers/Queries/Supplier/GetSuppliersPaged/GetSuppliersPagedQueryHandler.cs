using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.Suppliers;
using ecms.Application.Models.ViewModels.Suppliers;
using MediatR;
using SharedKernel;

namespace UnitTests.Handlers.Queries.GetSuppliersPaged;

public class GetSuppliersPagedQueryHandler(IApplicationDbContext _applicationDbContext,
                                           IMapper _mapper) : IRequestHandler<GetSuppliersPagedQuery, Result<PagedSupplierViewModel>>
{
    public async Task<Result<PagedSupplierViewModel>> Handle(GetSuppliersPagedQuery request, CancellationToken ct)
    {
        var suppliers = _applicationDbContext.Suppliers.ToList();
        var model = new PagedSupplierViewModel();

        model.TotalCount = suppliers.Count();

        if (request.CurrentPage > 0 && request.PageSize > 0)
        {
            suppliers = suppliers.Skip(request.CurrentPage * request.PageSize - request.PageSize)
                                 .Take(request.PageSize)
                                 .ToList();
        }

        var supplierDtos = _mapper.Map<List<SupplierDto>>(suppliers);
        model.Suppliers = supplierDtos;

        return Result.Success(model);
    }
}