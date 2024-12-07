namespace ecms.Application.Models.Dtos.Suppliers;

public class CreateSupplierContactDto
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public bool IsCommon { get; set; }

    public int SupplierId { get; set; }

    public string RepresentativeName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}