using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using ecms.Domain.Errors.Suppliers;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteSupplier;

public class DeleteSupplierCommandHandler(IApplicationDbContext _applicationDbContext,
                                          IMapper _mapper,
                                          ICurrentUserService _userService,
                                          IDateTimeProvider _dateTimeProvider) : IRequestHandler<DeleteSupplierCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteSupplierCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync(ct);
        try
        {
            var supplierToDelete = _applicationDbContext.Suppliers.FirstOrDefault(p => p.Id == request.SupplierId);
            if (supplierToDelete == null)
            {
                return Result.Failure<bool>(Error.NotFound(SupplierErrorCodes.NotFound, $"Supplier with {request.SupplierId} has not been found"));
            }

            var supplierContacts = _applicationDbContext.SupplierContacts.Where(p => p.SupplierId == request.SupplierId);
            foreach (var contact in supplierContacts)
            {
                contact.IsActive = false;
            }

            supplierToDelete.IsDeleted = true;

            var supplierHistory = _mapper.Map<SupplierHistoryEntity>(supplierToDelete);
            supplierHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            supplierHistory.CreatorUserId = _userService.UserId;
            await _applicationDbContext.SupplierHistories.AddAsync(supplierHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            transaction.Commit();
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}