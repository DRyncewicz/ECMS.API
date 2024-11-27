using ecms.Domain.ValueObjects;
using SharedKernel;

namespace ecms.Domain.Entities;

public class InvoiceEntity : Entity
{
    public string InvoiceNumber { get; set; }

    public int SupplierOrderId { get; set; }

    public Price TotalPrice { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public Guid FileGuid { get; set; }

    public virtual SupplierOrderEntity SupplierOrder { get; set; }
}