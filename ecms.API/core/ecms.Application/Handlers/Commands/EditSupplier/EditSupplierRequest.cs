using ecms.Application.Models.Dtos.Suppliers;

namespace ecms.Application.Handlers.Commands.EditSupplier;

public class EditSupplierRequest
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int AddressId { get; set; }

    public List<CreateSupplierContactDto> Contacts { get; set; } = [];
}