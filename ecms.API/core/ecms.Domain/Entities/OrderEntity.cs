using ecms.Domain.Enums;
using SharedKernel;

namespace ecms.Domain.Entities;

public class OrderEntity : Entity
{
    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public int OrderNumber { get; set; }

    public OrderStatusType OrderStatus { get; set; }

    public int? AddressId { get; set; }

    public int? TableNumber { get; set; }

    public double TotalPrice { get; set; }

    public string UserId { get; set; }

    public virtual StockTransactionEntity StockTransaction { get; set; }

    public virtual AddressEntity Address { get; set; }

    public virtual ICollection<OrderProductVariantEntity> OrderProductVariants { get; set; }
}
