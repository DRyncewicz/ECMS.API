using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Abstractions.Emails;
using ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;
using ecms.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernal;
using SharedKernel;

public class EditSupplierOrderCommandHandler(IApplicationDbContext _applicationDbContext,
                                             IMapper _mapper,
                                             IDateTimeProvider _dateTimeProvider,
                                             IEmailGenerator _emailGenerator) : IRequestHandler<EditSupplierOrderCommand, Result<int>>
{
    public async Task<Result<int>> Handle(EditSupplierOrderCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync(ct);
        try
        {
            var supplierOrderToEdit = _applicationDbContext.SupplierOrders.FirstOrDefault(p => p.Id == request.SupplierOrderId);
            Ensure.NotNull(supplierOrderToEdit);
            supplierOrderToEdit.EditDateTimeUtc = _dateTimeProvider.UtcNow;
            _mapper.Map(request, supplierOrderToEdit);
            await _applicationDbContext.SaveChangesAsync(ct);

            var existingSupplierOrderMaterials = _applicationDbContext.SupplierOrderMaterials.AsNoTracking().Where(p => p.SupplierOrderId == request.SupplierOrderId);
            var supplierOrderMaterialToAddDtos = request.EditSupplierOrderMaterialDtos.Where(p => p.SupplierOrderMaterialId == 0).ToList();
            var supplierOrderMaterialToEditDtos = request.EditSupplierOrderMaterialDtos.Where(p => existingSupplierOrderMaterials.Select(e => e.Id).Contains(p.SupplierOrderMaterialId)).ToList();
            var supplierOrderMaterialToRemoveEntities = existingSupplierOrderMaterials.Where(p => !request.EditSupplierOrderMaterialDtos.Select(d => d.SupplierOrderMaterialId).Contains(p.Id)).ToList();

            if (supplierOrderMaterialToAddDtos.Any())
            {
                var supplierOrderMaterialToAddEntities = _mapper.Map<List<SupplierOrderMaterialEntity>>(supplierOrderMaterialToAddDtos);
                supplierOrderMaterialToAddEntities.ForEach(p => p.SupplierOrderId = request.SupplierOrderId);
                _applicationDbContext.SupplierOrderMaterials.AddRange(supplierOrderMaterialToAddEntities);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            if (supplierOrderMaterialToEditDtos.Any())
            {
                var supplierOrderMaterialToEditEntities = _mapper.Map<List<SupplierOrderMaterialEntity>>(supplierOrderMaterialToEditDtos);
                supplierOrderMaterialToEditEntities.ForEach(p => p.SupplierOrderId = request.SupplierOrderId);
                _applicationDbContext.SupplierOrderMaterials.UpdateRange(supplierOrderMaterialToEditEntities);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            if (supplierOrderMaterialToRemoveEntities.Any())
            {
                _applicationDbContext.SupplierOrderMaterials.RemoveRange(supplierOrderMaterialToRemoveEntities);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            if (request.SendMessage == true)
            {
                var updatedSupplierOrderMaterials = supplierOrderMaterialToAddDtos.Union(supplierOrderMaterialToEditDtos);
                var emailAddress = _applicationDbContext.SupplierContacts.First(p => p.Id == request.SupplierContactId).Email;
                var materialSummary = _applicationDbContext.Materials.Where(p => updatedSupplierOrderMaterials.Select(m => m.MaterialId).Contains(p.Id))
                                                                     .ToDictionary(p => p.Name, p => updatedSupplierOrderMaterials.First(m => m.MaterialId == p.Id).Quantity);

                var emailMessage = new MessageEntity()
                {
                    Address = emailAddress,
                    Content = string.Concat(_emailGenerator.GenerateEditedOrderWelcomeMessageContent(request.Language, supplierOrderToEdit.Id, request.DeliveryDate),
                                            _emailGenerator.GenerateSupplierOrderHtmlTable(materialSummary, request.Language)),
                    Subject = _emailGenerator.GenerateSupplierOrderSubject(request.Language),
                    CreateDateTimeUtc = _dateTimeProvider.UtcNow,
                    ErrorAttempts = 0,
                    MessageStatus = ecms.Domain.Enums.MessageStatusType.Queued,
                    SentDateTimeUtc = _dateTimeProvider.UtcNow,
                };
                await _applicationDbContext.Messages.AddAsync(emailMessage, ct);
                await _applicationDbContext.SaveChangesAsync(ct);
                supplierOrderToEdit.MessageId = emailMessage.Id;
            }

            transaction.Commit();
            return Result.Success(supplierOrderToEdit.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}