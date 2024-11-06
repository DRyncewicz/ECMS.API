using ecms.Domain.Enums;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateMaterial;

public class CreateMaterialCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = string.Empty;

    public UnitOfMeasureType UnitOfMeasure {  get; set; }

    public string Description { get; set; } = string.Empty;

    public double MinStockLevel { get; set; }

    public double MaxStockLevel { get; set; }

    public double ReorderLevel { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? FileGuid { get; set; }

    public string? BatchNumber { get; set; }

    public int StockId { get; set; }
}
