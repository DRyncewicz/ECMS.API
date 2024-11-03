using SharedKernel;

namespace ecms.Domain.Entities;

public class ProductVariantAllergenEntity : Entity
{
    public int AllergenId { get; set; }

    public int ProductVariantId { get; set; }

    public virtual ProductVariantEntity ProductVariant { get; set; }

    public virtual AllergenEntity Allergen { get; set; }
}