using SharedKernel;

namespace ecms.Domain.Entities;

public class SupplierEntity : Entity
{
    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; }

    public int AddressId { get; set; }

    public virtual ICollection<SupplierOrderEntity> SupplierOrders { get; set; }

    public virtual AddressEntity Address { get; set; }

    public virtual ICollection<SupplierContactEntity> SupplierContacts { get; set; }

    public virtual ICollection<SupplierHistoryEntity> SupplierHistories { get; set; }
}