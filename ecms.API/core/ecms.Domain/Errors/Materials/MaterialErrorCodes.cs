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

    public const string MissingId = nameof(MissingId);

    public const string MissingProductVariantId = nameof(MissingProductVariantId);

    public const string InvalidProductMaterialsAmount = nameof(InvalidProductMaterialsAmount);

    public const string InvalidQuantity = nameof(InvalidQuantity);

    public const string MissingQuantity = nameof(MissingQuantity);
}