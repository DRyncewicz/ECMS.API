using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Errors.SupplierOrders;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;

public class CreateSupplierOrderMaterialDtoValidator : AbstractValidator<CreateSupplierOrderMaterialDto>
{
    public CreateSupplierOrderMaterialDtoValidator()
    {
        RuleFor(p => p.MaterialId)
            .NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingMaterialId);

        RuleFor(p => p.Quantity).GreaterThan(0).WithErrorCode(SupplierOrderErrorCodes.InvalidQuantity);
    }
}