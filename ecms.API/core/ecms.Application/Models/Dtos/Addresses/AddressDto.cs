namespace ecms.Application.Models.Dtos.Addresses;

public class AddressDto
{
    public int AddressId { get; set; }

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string BuildingNumber { get; set; } = string.Empty;

    public string? ApartmentNumber { get; set; } = string.Empty;
}
