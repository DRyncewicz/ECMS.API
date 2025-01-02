using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.Supplier.CreateSupplier;

public class CreateSupplierCommandHandler(IApplicationDbContext _applicationDbContext,
                                          IMapper _mapper,
                                          IDateTimeProvider _dateTimeProvider,
                                          ICurrentUserService _userService) : IRequestHandler<CreateSupplierCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateSupplierCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync(ct);
        try
        {
            var supplierEntity = _mapper.Map<SupplierEntity>(request);
            await _applicationDbContext.Suppliers.AddAsync(supplierEntity, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var supplierHistory = _mapper.Map<SupplierHistoryEntity>(supplierEntity);
            supplierHistory.CreatorUserId = _userService.UserId;
            supplierHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            await _applicationDbContext.SupplierHistories.AddAsync(supplierHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            transaction.Commit();
            return Result.Success(supplierEntity.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}
