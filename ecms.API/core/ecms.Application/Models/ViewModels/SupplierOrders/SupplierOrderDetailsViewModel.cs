using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Application.Models.Dtos.Suppliers;
using ecms.Domain.Enums;

namespace ecms.Application.Models.ViewModels.SupplierOrders;

public class SupplierOrderDetailsViewModel
{
    public int SupplierOrderId { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public bool SendMessage { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public DateTimeOffset EditDateTimeUtc { get; set; }

    public StatusType Status { get; set; }

    public int? MessageId { get; set; }

    public IEnumerable<SupplierOrderMaterialDto> SupplierOrderMaterialDtos { get; set; } = [];

    public SupplierContactDto SupplierContactDto { get; set; } = new SupplierContactDto();

    public SupplierDto SupplierDto { get; set; } = new SupplierDto();
}