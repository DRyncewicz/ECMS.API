using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecms.Infrastructure.Database.Seeds;

public static class CategorySeed
{
    public static void DataSeed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>()
                    .HasData(new CategoryEntity()
                    {
                        Id = 1,
                        HierarchyId = new HierarchyId(),
                        Name = "Main"
                    });
    }
}
