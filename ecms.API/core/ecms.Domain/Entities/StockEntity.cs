using SharedKernel;

namespace ecms.Domain.Entities;

public class StockEntity : Entity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsDeleted { get; set; }

    public int AddressId { get; set; }

    public virtual AddressEntity Address { get; set; }

    public virtual ICollection<StockLevelEntity> StockLevels { get; set; }
}