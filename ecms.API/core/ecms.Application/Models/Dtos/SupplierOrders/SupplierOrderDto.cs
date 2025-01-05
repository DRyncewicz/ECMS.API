using ecms.Domain.Enums;
using ecms.Domain.ValueObjects;

namespace ecms.Application.Models.Dtos.SupplierOrders;

public class SupplierOrderDto
{
    public int SupplierOrderId { get; set; }

    public int SupplierId { get; set; }

    public int SupplierContactId { get; set; }

    public bool SendMessage { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public DateTimeOffset? EditDateTimeUtc { get; set; }

    public StatusType Status { get; set; }

    public int? MessageId { get; set; }

    public Price TotalPrice { get; set; }
}