using ecms.Application.Models.Dtos.Suppliers;
using ecms.Domain.Errors.SupplierOrders;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.CreateSupplierOrder;

public class CreateSupplierOrderMaterialDtoValidator : AbstractValidator<CreateSupplierOrderMaterialDto>
{
    public CreateSupplierOrderMaterialDtoValidator()
    {
        RuleFor(p => p.MaterialId)
            .NotEmpty().WithErrorCode(SupplierOrderErrorCodes.MissingMaterialId);

        RuleFor(p => p.Quantity).GreaterThan(0).WithErrorCode(SupplierOrderErrorCodes.InvalidQuantity);

        RuleFor(p => p.PricePerUnit.Amount).GreaterThan(0).WithErrorCode(SupplierOrderErrorCodes.InvalidPricePerUnit);
    }
}