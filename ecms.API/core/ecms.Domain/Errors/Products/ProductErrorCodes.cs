namespace ecms.Domain.Errors.Products;

public static class ProductErrorCodes
{
    public const string MissingName = nameof(MissingName);

    public const string InvalidLengthName = nameof(InvalidLengthName);

    public const string MissingDescription = nameof(MissingDescription);

    public const string InvalidLengthDescription = nameof(InvalidLengthDescription);

    public const string MissingCategoryId = nameof(MissingCategoryId);

    public const string MissingVat = nameof(MissingVat);

    public const string MissingUnit = nameof(MissingUnit);

    public const string MissingAlcoholContent = nameof(MissingAlcoholContent);

    public const string EmptyVariants = nameof(EmptyVariants);

    public const string MissingId = nameof(MissingId);
}
