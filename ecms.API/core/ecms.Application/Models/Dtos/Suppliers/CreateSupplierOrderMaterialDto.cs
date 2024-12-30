using ecms.Domain.ValueObjects;

namespace ecms.Application.Models.Dtos.Suppliers;

public class CreateSupplierOrderMaterialDto
{
    public int MaterialId { get; set; }

    public double Quantity { get; set; }

    public Price PricePerUnit { get; set; } = new Price(0, Currency.Usd);

    public double Discount { get; set; }
}