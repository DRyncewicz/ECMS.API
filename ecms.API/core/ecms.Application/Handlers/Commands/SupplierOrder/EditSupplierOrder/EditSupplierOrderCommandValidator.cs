using ecms.Domain.Errors.SupplierOrders;
using FluentValidation;
using SharedKernal;

namespace ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;

public class EditSupplierOrderCommandValidator : AbstractValidator<EditSupplierOrderCommand>
{
    public EditSupplierOrderCommandValidator(IDateTimeProvider _dateTimeProvider)
    {
        RuleFor(p => p.DeliveryDate).Must(p => p.Value > _dateTimeProvider.UtcNow.AddHours(1))
            .When(p => p.DeliveryDate != null)
            .WithErrorCode(SupplierOrderErrorCodes.InvalidDeliveryDate);

        RuleFor(p => p.SupplierId).NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingSupplierId);

        RuleFor(p => p.SupplierOrderId).NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingSupplierOrderId);

        RuleFor(p => p.Language).NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingLanguage)
            .IsInEnum();

        RuleForEach(p => p.EditSupplierOrderMaterialDtos).SetValidator(new EditSupplierOrderMaterialDtoValidator());
    }
}