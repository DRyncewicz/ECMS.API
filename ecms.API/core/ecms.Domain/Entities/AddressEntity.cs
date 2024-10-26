using SharedKernel;

namespace ecms.Domain.Entities;

public class AddressEntity : Entity
{
    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string PostalCode { get; set; }

    public string BuildingNumber { get; set; }

    public string? ApartmentNumber { get; set; }

    public virtual ICollection<StockEntity> Stocks { get; set; }

    public virtual ICollection<SupplierEntity> Suppliers { get; set; }

    public virtual ICollection<OrderEntity> Orders { get; set; }
}