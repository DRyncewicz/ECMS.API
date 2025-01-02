using ecms.Domain.Enums;
using SharedKernel;

namespace ecms.Domain.Entities;

public class SupplierOrderEntity : Entity
{
    public int SupplierId { get; set; }

    public DateTimeOffset? DeliveryDate { get; set; }

    public StatusType Status { get; set; }

    public int? MessageId { get; set; }

    public virtual SupplierEntity Supplier { get; set; }

    public virtual MessageEntity Message { get; set; }

    public virtual ICollection<InvoiceEntity> Invoices { get; set; }

    public virtual ICollection<SupplierOrderMaterialEntity> SupplierOrderMaterials { get; set; }
}