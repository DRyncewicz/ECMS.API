using ecms.Domain.Errors.SupplierOrders;
using FluentValidation;
using SharedKernal;

namespace ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;

public class CreateSupplierOrderCommandValidator : AbstractValidator<CreateSupplierOrderCommand>
{
    public CreateSupplierOrderCommandValidator(IDateTimeProvider _dateTimeProvider)
    {
        RuleFor(p => p.DeliveryDate).Must(p => p.HasValue && p.Value > _dateTimeProvider.UtcNow.AddHours(1))
            .When(p => p.DeliveryDate != null)
            .WithErrorCode(SupplierOrderErrorCodes.InvalidDeliveryDate);

        RuleFor(p => p.SupplierId).NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingSupplierId);

        RuleFor(p => p.Language).NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingLanguage)
            .IsInEnum();

        RuleForEach(p => p.SupplierOrderMaterialDtos).SetValidator(new CreateSupplierOrderMaterialDtoValidator());
    }
}