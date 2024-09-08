using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ecms.Domain.Entities;

public class CategoryEntity : Entity
{
    public HierarchyId HierarchyId { get; set; }

    public string Name { get; set; }

    public Guid? FileGuid { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; }
}
