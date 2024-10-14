using ecms.Domain.Enums;
using SharedKernel;

namespace ecms.Domain.Entities;

public class ProductHistoryEntity : Entity
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int CategoryId { get; set; }

    public int Vat { get; set; }

    public UnitType Unit { get; set; }

    public AlcoholContentType AlcoholContent { get; set; }

    public GtuCodeType? GtuCode { get; set; }

    public Guid? FileGuid { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset CreateDateTimeUtc { get; set; }

    public string CreatorUserId { get; set; }

    public virtual ProductEntity Product { get; set; }
}