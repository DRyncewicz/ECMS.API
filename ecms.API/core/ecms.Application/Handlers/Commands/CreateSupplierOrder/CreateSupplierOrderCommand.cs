using ecms.Application.Models.Dtos.Suppliers;
using ecms.Domain.Enums;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateSupplierOrder;

public class CreateSupplierOrderCommand : IRequest<Result<int>>
{
    public IEnumerable<CreateSupplierOrderMaterialDto> SupplierOrderMaterialDtos { get; set; } = [];

    public int SupplierId { get; set; }

    public int SupplierContactId { get; set; }

    public LanguageType Language { get; set; }

    public DateTimeOffset? DeliveryDate { get; set; }

    public bool SendMessage { get; set; }
}