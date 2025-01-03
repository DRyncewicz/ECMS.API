using ecms.Application.Models.Dtos.SupplierOrders;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.EditSupplier;

public class EditSupplierCommand : IRequest<Result<int>>
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int AddressId { get; set; }

    public List<CreateSupplierContactDto> Contacts { get; set; } = [];

    public EditSupplierCommand(EditSupplierRequest request, int id)
    {
        SupplierId = id;
        Name = request.Name;
        AddressId = request.AddressId;
        Contacts = request.Contacts;
    }

    public EditSupplierCommand()
    {
    }
}