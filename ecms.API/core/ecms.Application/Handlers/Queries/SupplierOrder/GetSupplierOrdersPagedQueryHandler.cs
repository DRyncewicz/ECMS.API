using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Application.Models.ViewModels.SupplierOrders;
using ecms.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.SupplierOrder;

public class GetSupplierOrdersPagedQueryHandler(IApplicationDbContext _applicationDbContext,
                                                IMapper _mapper) : IRequestHandler<GetSupplierOrdersPagedQuery, Result<SupplierOrdersViewModel>>
{
    public async Task<Result<SupplierOrdersViewModel>> Handle(GetSupplierOrdersPagedQuery request, CancellationToken ct)
    {
        var supplierOrders = _applicationDbContext.SupplierOrders.Include(p => p.SupplierOrderMaterials).OrderByDescending(p => p.CreateDateTimeUtc).ToList();
        await _applicationDbContext.SaveChangesAsync(ct);

        var model = new SupplierOrdersViewModel();
        model.TotalCount = supplierOrders.Count();

        if (request.CurrentPage > 0 && request.PageSize > 0)
        {
            supplierOrders = supplierOrders.Skip(request.CurrentPage * request.PageSize - request.PageSize)
                                           .Take(request.PageSize)
                                           .ToList();
        }

        model.SupplierOrders = supplierOrders.Select(supplierOrder =>
        {
            var totalAmount = supplierOrder.SupplierOrderMaterials.Sum(material => material.PricePerUnit.Amount * material.Quantity);

            var supplierOrderDto = _mapper.Map<SupplierOrderDto>(supplierOrder);
            supplierOrderDto.TotalPrice = new Price(totalAmount, supplierOrder.SupplierOrderMaterials.First().PricePerUnit.Currency);

            return supplierOrderDto;
        });

        return Result.Success(model);
    }
}