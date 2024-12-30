namespace ecms.Domain.Errors.SupplierOrders;

public class SupplierOrderErrorCodes
{
    public const string MissingName = nameof(MissingName);

    public const string InvalidLengthName = nameof(InvalidLengthName);

    public const string MissingAddressId = nameof(MissingAddressId);

    public const string NotFound = nameof(NotFound);

    public const string InvalidDeliveryDate = nameof(InvalidDeliveryDate);

    public const string MissingSupplierId = nameof(MissingSupplierId);

    public const string MissingLanguage = nameof(MissingLanguage);

    public const string InvalidLanguageType = nameof(InvalidLanguageType);

    public const string MissingMaterialId = nameof(MissingMaterialId);

    public const string InvalidQuantity = nameof(InvalidQuantity);

    public const string InvalidPricePerUnit = nameof(InvalidPricePerUnit);
}