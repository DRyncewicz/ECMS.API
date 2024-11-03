using SharedKernel;

namespace ecms.Domain.Entities;

public class StockLevelEntity : Entity
{
    public int MaterialId { get; set; }

    public int StockId { get; set; }

    public int Quantity { get; set; }

    public bool IsDeleted { get; set; }

    public string? BatchNumber { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public DateTimeOffset? LastUpdated { get; set; }

    public virtual StockEntity Stock { get; set; }

    public virtual MaterialEntity Material { get; set; }

    public virtual ICollection<StockTransactionEntity> StockTransactions { get; set; }
}