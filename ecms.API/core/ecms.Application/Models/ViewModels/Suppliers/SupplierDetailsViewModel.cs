using ecms.Application.Models.Dtos.Suppliers;

namespace ecms.Application.Models.ViewModels.Suppliers;

public class SupplierDetailsViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public int AddressId { get; set; }

    public List<SupplierContactDto> ContactDtos { get; set; } = [];
}
