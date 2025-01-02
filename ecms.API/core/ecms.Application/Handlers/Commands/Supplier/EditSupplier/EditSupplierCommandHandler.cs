using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.EditSupplier;

public class EditSupplierCommandHandler(IApplicationDbContext _applicationDbContext,
                                        IMapper _mapper,
                                        IDateTimeProvider _dateTimeProvider,
                                        ICurrentUserService _userService) : IRequestHandler<EditSupplierCommand, Result<int>>
{
    public async Task<Result<int>> Handle(EditSupplierCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync();
        try
        {
            var supplierToEdit = _applicationDbContext.Suppliers.FirstOrDefault(p => p.Id == request.SupplierId);
            _mapper.Map(request, supplierToEdit);
            Ensure.NotNull(supplierToEdit);
            await _applicationDbContext.SaveChangesAsync(ct);

            var supplierHistory = _mapper.Map<SupplierHistoryEntity>(supplierToEdit);
            supplierHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            supplierHistory.CreatorUserId = _userService.UserId;
            await _applicationDbContext.SupplierHistories.AddAsync(supplierHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var existingSupplierContacts = _applicationDbContext.SupplierContacts.AsNoTracking().Where(p => p.SupplierId == request.SupplierId);
            var contactToAddDtos = request.Contacts.Where(p => p.Id == 0);
            var contactToEditDtos = request.Contacts.Where(p => existingSupplierContacts.Select(p => p.Id).Contains(p.Id)).ToList();
            var contactToRemoveEntities = existingSupplierContacts.Where(p => !request.Contacts.Select(p => p.Id).Contains(p.Id)).ToList();

            if (contactToAddDtos.Any())
            {
                var contactToAddEntites = _mapper.Map<List<SupplierContactEntity>>(contactToAddDtos);
                _applicationDbContext.SupplierContacts.AddRange(contactToAddEntites);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            if (contactToEditDtos.Any())
            {
                var contactToEditEntites = _mapper.Map<List<SupplierContactEntity>>(contactToEditDtos);
                _applicationDbContext.SupplierContacts.UpdateRange(contactToEditEntites);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            if (contactToRemoveEntities.Any())
            {
                _applicationDbContext.SupplierContacts.RemoveRange(contactToRemoveEntities);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            transaction.Commit();
            return Result.Success(supplierToEdit.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}