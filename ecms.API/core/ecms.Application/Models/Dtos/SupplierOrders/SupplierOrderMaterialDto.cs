using ecms.Application.Models.Dtos.Materials;
using ecms.Domain.ValueObjects;

namespace ecms.Application.Models.Dtos.SupplierOrders;

public class SupplierOrderMaterialDto
{
    public int SupplierOrderMaterialId { get; set; }

    public int SupplierOrderId { get; set; }

    public MaterialDto Material { get; set; } = new MaterialDto();

    public bool IsDelivered { get; set; }

    public Price PricePerUnit { get; set; }

    public double Quantity { get; set; }
}