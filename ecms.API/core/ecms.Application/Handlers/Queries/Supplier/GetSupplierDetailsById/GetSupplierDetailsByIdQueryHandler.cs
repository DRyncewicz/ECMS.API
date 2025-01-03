using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.ViewModels.Suppliers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.Supplier.GetSupplierDetailsById;

public class GetSupplierDetailsByIdQueryHandler(IApplicationDbContext _applicationDbContext,
                                                IMapper _mapper) : IRequestHandler<GetSupplierDetailsByIdQuery, Result<SupplierDetailsViewModel>>
{
    public async Task<Result<SupplierDetailsViewModel>> Handle(GetSupplierDetailsByIdQuery request, CancellationToken ct)
    {
        var supplier = _applicationDbContext.Suppliers.Include(p => p.SupplierContacts)
                                                      .AsNoTracking()
                                                      .FirstOrDefault(p => p.Id == request.SupplierId);

        if (supplier is null)
        {
            return Result.Failure<SupplierDetailsViewModel>(Error.NotFound("404", $"There is no record with ID {request.SupplierId}"));
        }

        var model = _mapper.Map<SupplierDetailsViewModel>(supplier);
        return Result.Success(model);
    }
}