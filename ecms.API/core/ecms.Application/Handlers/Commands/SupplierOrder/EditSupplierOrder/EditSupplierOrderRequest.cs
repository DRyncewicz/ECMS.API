using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Enums;

namespace ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;

public class EditSupplierOrderRequest
{
    public int SupplierOrderId { get; set; }

    public int SupplierId { get; set; }

    public int SupplierContactId { get; set; }

    public LanguageType Language { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public bool SendMessage { get; set; }

    public IEnumerable<EditSupplierOrderMaterialDto> EditSupplierOrderMaterialDtos { get; set; } = [];
}