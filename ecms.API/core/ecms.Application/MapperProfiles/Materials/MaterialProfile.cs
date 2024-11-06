using AutoMapper;
using ecms.Application.Handlers.Commands.CreateMaterial;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.Materials;

public class MaterialProfile : Profile
{
    public MaterialProfile()
    {
        CreateMap<CreateMaterialCommand, MaterialEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UnitOfMeasure, opt => opt.MapFrom(src => src.UnitOfMeasure))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.MinStockLevel, opt => opt.MapFrom(src => src.MinStockLevel))
            .ForMember(dest => dest.MaxStockLevel, opt => opt.MapFrom(src => src.MaxStockLevel))
            .ForMember(dest => dest.ReorderLevel, opt => opt.MapFrom(src => src.ReorderLevel))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.StockLevel, opt => opt.Ignore())
            .ForMember(dest => dest.ProductMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialHistories, opt => opt.Ignore())
            .ForMember(dest => dest.SuppliersOrderMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CreateMaterialCommand, StockLevelEntity>()
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.StockId))
            .ForMember(dest => dest.BatchNumber, opt => opt.MapFrom(src => src.BatchNumber))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.MaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.StockTransactions, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdated, opt => opt.Ignore())
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<MaterialEntity, MaterialHistoryEntity>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.MaxStockLevel, opt => opt.MapFrom(src => src.MaxStockLevel))
            .ForMember(dest => dest.MinStockLevel, opt => opt.MapFrom(src => src.MinStockLevel))
            .ForMember(dest => dest.UnitOfMeasure, opt => opt.MapFrom(src => src.UnitOfMeasure))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReorderLevel, opt => opt.MapFrom(src => src.ReorderLevel))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());            
    }
}
