using ecms.Domain.Errors.Stocks;
using FluentValidation;

namespace ecms.Application.Handlers.Commands.CreateStock;

public class CreateStockCommandValidator : AbstractValidator<CreateStockCommand>
{
    private const int MaximumLengthName = 50;
    private const int MaximumLengthDescription = 500;

    public CreateStockCommandValidator()
    {
        RuleFor(p => p.Name)
            .MaximumLength(MaximumLengthName).WithErrorCode(StockErrorCodes.InvalidLengthName)
            .NotEmpty().WithErrorCode(StockErrorCodes.MissingName);

        RuleFor(p => p.Description)
            .MaximumLength(MaximumLengthDescription).WithErrorCode(StockErrorCodes.InvalidLengthDescription);

        RuleFor(p => p.AddressId)
            .NotEmpty().WithErrorCode(StockErrorCodes.MissingAddressId);
    }
}