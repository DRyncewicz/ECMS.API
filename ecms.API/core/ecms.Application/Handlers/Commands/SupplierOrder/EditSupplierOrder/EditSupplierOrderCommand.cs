using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Enums;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;

public class EditSupplierOrderCommand : IRequest<Result<int>>
{
    public int SupplierOrderId { get; set; }

    public int SupplierId { get; set; }

    public int SupplierContactId { get; set; }

    public LanguageType Language { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public bool SendMessage { get; set; }

    public IEnumerable<EditSupplierOrderMaterialDto> EditSupplierOrderMaterialDtos { get; set; } = [];

    public EditSupplierOrderCommand(EditSupplierOrderRequest request, int id)
    {
        SupplierOrderId = id;
        SupplierId = request.SupplierId;
        SupplierContactId = request.SupplierContactId;
        Language = request.Language;
        DeliveryDate = request.DeliveryDate;
        SendMessage = request.SendMessage;
        EditSupplierOrderMaterialDtos = request.EditSupplierOrderMaterialDtos;
    }

    public EditSupplierOrderCommand()
    {
    }
}