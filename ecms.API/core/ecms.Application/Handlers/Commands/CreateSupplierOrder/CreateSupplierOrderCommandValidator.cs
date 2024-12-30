using ecms.Domain.Errors.SupplierOrders;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.CreateSupplierOrder;

public class CreateSupplierOrderCommandValidator : AbstractValidator<CreateSupplierOrderCommand>
{
    public CreateSupplierOrderCommandValidator()
    {
        RuleFor(p => p.DeliveryDate).Must(p => p.HasValue && p.Value > DateTime.UtcNow.AddHours(1)).WithErrorCode(SupplierOrderErrorCodes.InvalidDeliveryDate);

        RuleFor(p => p.SupplierId).NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingSupplierId);

        RuleFor(p => p.Language).NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingLanguage)
            .IsInEnum();

        RuleForEach(p => p.SupplierOrderMaterialDtos).SetValidator(new CreateSupplierOrderMaterialDtoValidator());
    }
}