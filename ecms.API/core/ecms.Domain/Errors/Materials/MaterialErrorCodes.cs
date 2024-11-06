namespace ecms.Domain.Errors.Materials;

public static class MaterialErrorCodes
{
    public const string MissingName = nameof(MissingName);

    public const string InvalidLengthName = nameof(InvalidLengthName);

    public const string MissingDescription = nameof(MissingDescription);

    public const string InvalidLengthDescription = nameof(InvalidLengthDescription);

    public const string MissingUnitOfMeasure = nameof(MissingUnitOfMeasure);

    public const string InvalidMaxStockLevel = nameof(InvalidMaxStockLevel);

    public const string InvalidMinStockLevel = nameof(InvalidMinStockLevel);

    public const string InvalidReorderLevel = nameof(InvalidReorderLevel);
}
