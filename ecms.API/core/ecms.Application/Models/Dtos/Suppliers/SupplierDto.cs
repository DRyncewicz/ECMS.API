namespace ecms.Application.Models.Dtos.Suppliers;

public class SupplierDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public int AddressId { get; set; }
}