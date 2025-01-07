using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Errors.SupplierOrders;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;

public class EditSupplierOrderMaterialDtoValidator : AbstractValidator<EditSupplierOrderMaterialDto>
{
    public EditSupplierOrderMaterialDtoValidator()
    {
        RuleFor(p => p.MaterialId)
            .NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingMaterialId);

        RuleFor(p => p.Quantity).GreaterThan(0).WithErrorCode(SupplierOrderErrorCodes.InvalidQuantity);
    }
}