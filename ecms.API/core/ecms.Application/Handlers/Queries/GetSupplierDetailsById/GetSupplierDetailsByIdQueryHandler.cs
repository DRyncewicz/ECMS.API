using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Models.ViewModels.Suppliers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetSupplierDetailsById;

public class GetSupplierDetailsByIdQueryHandler(IApplicationDbContext _applicationDbContext,
                                                IMapper _mapper) : IRequestHandler<GetSupplierDetailsByIdQuery, Result<SupplierDetailsViewModel>>
{
    public async Task<Result<SupplierDetailsViewModel>> Handle(GetSupplierDetailsByIdQuery request, CancellationToken ct)
    {
        var supplier = _applicationDbContext.Suppliers.Include(p => p.SupplierContacts).AsNoTracking().FirstOrDefault(p => p.Id == request.SupplierId);
        Ensure.NotNull(supplier);
        var model = _mapper.Map<SupplierDetailsViewModel>(supplier);
        return Result.Success(model);
    }
}
