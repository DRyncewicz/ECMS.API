using ecms.Domain.ValueObjects;
using SharedKernel;

namespace ecms.Domain.Entities;

public class InvoiceEntity : Entity
{
    public string InvoiceNumber { get; set; }

    public Price TotalPrice { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public Guid FileGuid { get; set; }

    public virtual ICollection<SupplierOrderEntity> SupplierOrders { get; set; }
}
