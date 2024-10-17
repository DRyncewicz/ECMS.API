using ecms.Domain.Enums;
using SharedKernel;

namespace ecms.Domain.Entities;

public class StockTransactionEntity : Entity
{
    public int MaterialId { get; set; }

    public int StockLevelId { get; set; }

    public int Quantity { get; set; }

    public TransactionType TransactionType { get; set; }

    public int? SupplierOrderId { get; set; }

    public int? OrderId { get; set; }

    public string? BatchNumber { get; set; }

    public DateTimeOffset? ExpiryDate { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public string UserId { get; set; }

    public virtual StockLevelEntity StockLevel { get; set; }

    public virtual SupplierOrderMaterialEntity SupplierOrderMaterial { get; set; }

    public virtual OrderEntity Order { get; set; }
}
