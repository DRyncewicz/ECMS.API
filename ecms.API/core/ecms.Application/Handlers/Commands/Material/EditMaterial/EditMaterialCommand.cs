using ecms.Domain.Enums;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.Material.EditMaterial;

public class EditMaterialCommand : IRequest<Result<int>>
{
    public int MaterialId { get; set; }

    public string Name { get; set; } = string.Empty;

    public UnitOfMeasureType UnitOfMeasure { get; set; }

    public string Description { get; set; } = string.Empty;

    public double MinStockLevel { get; set; }

    public double MaxStockLevel { get; set; }

    public bool IsActive { get; set; }

    public double ReorderLevel { get; set; }

    public Guid? FileGuid { get; set; }

    public string? BatchNumber { get; set; }

    public int StockId { get; set; }

    public EditMaterialCommand(EditMaterialRequest request, int id)
    {
        MaterialId = id;
        Name = request.Name;
        Description = request.Description;
        MinStockLevel = request.MinStockLevel;
        MaxStockLevel = request.MaxStockLevel;
        UnitOfMeasure = request.UnitOfMeasure;
        IsActive = request.IsActive;
        ReorderLevel = request.ReorderLevel;
        FileGuid = request.FileGuid;
        BatchNumber = request.BatchNumber;
        StockId = request.StockId;
    }

    public EditMaterialCommand()
    {
    }
}