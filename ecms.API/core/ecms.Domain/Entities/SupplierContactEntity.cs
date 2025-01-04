using SharedKernel;

namespace ecms.Domain.Entities;

public class SupplierContactEntity : Entity
{
    public bool IsActive { get; set; }

    public bool IsCommon { get; set; }

    public int SupplierId { get; set; }

    public string RepresentativeName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Description { get; set; }

    public virtual SupplierEntity Supplier { get; set; }

    public virtual ICollection<SupplierOrderEntity> SupplierOrders { get; set; }
}