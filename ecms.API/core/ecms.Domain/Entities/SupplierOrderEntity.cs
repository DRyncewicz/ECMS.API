using ecms.Domain.Enums;
using SharedKernel;

namespace ecms.Domain.Entities;

public class SupplierOrderEntity : Entity
{
    public int SupplierId { get; set; }

    public int InvoiceId { get; set; }

    public DateTimeOffset? DeliveryDate { get; set; }

    public StatusType Status { get; set; }

    public virtual MaterialEntity Material { get; set; }

    public virtual InvoiceEntity Invoice { get; set; }

    public virtual StockTransactionEntity StockTransaction { get; set; }
}