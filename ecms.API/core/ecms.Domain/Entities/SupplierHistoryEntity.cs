using SharedKernel;

namespace ecms.Domain.Entities;

public class SupplierHistoryEntity : Entity
{
    public int SupplierId { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public int AddressId { get; set; }

    public string CreatorUserId { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public virtual SupplierEntity Supplier { get; set; }
}
