using ecms.Domain.ValueObjects;
using SharedKernel;

namespace ecms.Domain.Entities;

public class SupplierOrderMaterialEntity : Entity
{
    public int SupplierOrderId { get; set; }

    public int MaterialId { get; set; }

    public bool IsDelivered { get; set; }

    public Price PricePerUnit { get; set; }

    public int Quantity { get; set; }

    public virtual MaterialEntity Material { get; set; }

    public virtual StockTransactionEntity StockTransaction { get; set; }

    public virtual SupplierOrderEntity SupplierOrder { get; set; }
}
