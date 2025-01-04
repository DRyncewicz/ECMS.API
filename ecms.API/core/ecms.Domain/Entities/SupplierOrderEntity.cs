using ecms.Domain.Enums;
using SharedKernel;

namespace ecms.Domain.Entities;

public class SupplierOrderEntity : Entity
{
    public int SupplierId { get; set; }

    public int SupplierContactId { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public DateTimeOffset? EditDateTimeUtc { get; set; }

    public StatusType Status { get; set; }

    public int? MessageId { get; set; }

    public virtual SupplierEntity Supplier { get; set; }

    public virtual MessageEntity Message { get; set; }

    public virtual SupplierContactEntity SupplierContact { get; set; }

    public virtual ICollection<InvoiceEntity> Invoices { get; set; }

    public virtual ICollection<SupplierOrderMaterialEntity> SupplierOrderMaterials { get; set; }
}