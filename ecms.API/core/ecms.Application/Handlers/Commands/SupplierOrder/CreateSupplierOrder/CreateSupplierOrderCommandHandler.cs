using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Abstractions.Emails;
using ecms.Domain.Entities;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;

public class CreateSupplierOrderCommandHandler(IApplicationDbContext _applicationDbContext,
                                               IMapper _mapper,
                                               IDateTimeProvider _dateTimeProvider,
                                               IEmailGenerator _emailGenerator) : IRequestHandler<CreateSupplierOrderCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateSupplierOrderCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync();
        try
        {
            var supplierOrder = _mapper.Map<SupplierOrderEntity>(request);
            supplierOrder.Status = Domain.Enums.StatusType.Pending;
            await _applicationDbContext.SupplierOrders.AddAsync(supplierOrder, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var supplierOrderMaterials = _mapper.Map<List<SupplierOrderMaterialEntity>>(request.SupplierOrderMaterialDtos);
            supplierOrderMaterials.ForEach(p => p.SupplierOrderId = supplierOrder.Id);
            await _applicationDbContext.SupplierOrderMaterials.AddRangeAsync(supplierOrderMaterials, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            if (request.SendMessage == true)
            {
                var emailAddress = _applicationDbContext.SupplierContacts.First(p => p.Id == request.SupplierContactId).Email;
                var materialSummary = _applicationDbContext.Materials.Where(p => supplierOrderMaterials.Select(p => p.MaterialId)
                                                                                                       .Contains(p.Id))
                                                                                                       .ToDictionary(p => p.Name, p => supplierOrderMaterials
                                                                                                       .First(m => m.MaterialId == p.Id).Quantity);

                var emailMessage = new MessageEntity()
                {
                    Address = emailAddress,
                    Content = string.Concat(_emailGenerator.GenerateOrderWelcomeMessageContent(request.Language, request.DeliveryDate, supplierOrder.Id),
                                            _emailGenerator.GenerateSupplierOrderHtmlTable(materialSummary, request.Language)),
                    Subject = _emailGenerator.GenerateSupplierOrderSubject(request.Language),
                    CreateDateTimeUtc = _dateTimeProvider.UtcNow,
                    ErrorAttempts = 0,
                    MessageStatus = Domain.Enums.MessageStatusType.Queued,
                    SentDateTimeUtc = _dateTimeProvider.UtcNow,
                };
                await _applicationDbContext.Messages.AddAsync(emailMessage, ct);
                await _applicationDbContext.SaveChangesAsync(ct);
                supplierOrder.MessageId = emailMessage.Id;
            }
            transaction.Commit();
            return Result.Success(supplierOrder.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}