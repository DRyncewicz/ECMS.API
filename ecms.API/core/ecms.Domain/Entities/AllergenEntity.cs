using SharedKernel;

namespace ecms.Domain.Entities;

public class AllergenEntity : Entity
{
    public string Name { get; set; }

    public virtual ICollection<ProductVariantAllergenEntity> ProductVariantAllergens { get; set; }
}