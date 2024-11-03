using ecms.Domain.Errors.Stocks;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.EditStock;

public class EditStockCommandValidator : AbstractValidator<EditStockCommand>
{
    private const int MaximumLengthName = 50;
    private const int MaximumLengthDescription = 500;

    public EditStockCommandValidator()
    {
        RuleFor(p => p.StockId).NotEmpty().WithErrorCode(StockErrorCodes.MissingId);

        RuleFor(p => p.Name)
            .MaximumLength(MaximumLengthName).WithErrorCode(StockErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(StockErrorCodes.MissingName);

        RuleFor(p => p.Description)
            .MaximumLength(MaximumLengthDescription).WithErrorCode(StockErrorCodes.InvalidLengthDescription);

        RuleFor(p => p.AddressId)
            .NotEmpty().WithErrorCode(StockErrorCodes.MissingAddressId);
    }
}