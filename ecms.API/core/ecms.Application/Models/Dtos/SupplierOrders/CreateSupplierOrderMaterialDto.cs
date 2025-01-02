using ecms.Domain.ValueObjects;

namespace ecms.Application.Models.Dtos.SupplierOrders;

public class CreateSupplierOrderMaterialDto
{
    public int MaterialId { get; set; }

    public double Quantity { get; set; }

    public Price PricePerUnit { get; set; }

    public double Discount { get; set; }
}