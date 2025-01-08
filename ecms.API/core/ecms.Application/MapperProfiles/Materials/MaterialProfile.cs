using AutoMapper;
using ecms.Application.Handlers.Commands.Material.CreateMaterial;
using ecms.Application.Handlers.Commands.Material.EditMaterial;
using ecms.Application.Models.Dtos.Materials;
using ecms.Application.Models.ViewModels.Materials;
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

        CreateMap<EditMaterialCommand, MaterialEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MaterialId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UnitOfMeasure, opt => opt.MapFrom(src => src.UnitOfMeasure))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.MinStockLevel, opt => opt.MapFrom(src => src.MinStockLevel))
            .ForMember(dest => dest.MaxStockLevel, opt => opt.MapFrom(src => src.MaxStockLevel))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.ReorderLevel, opt => opt.MapFrom(src => src.ReorderLevel))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.StockLevel, opt => opt.Ignore())
            .ForMember(dest => dest.SuppliersOrderMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialHistories, opt => opt.Ignore())
            .ForMember(dest => dest.ProductMaterials, opt => opt.Ignore());

        CreateMap<MaterialEntity, MaterialHistoryEntity>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.MaxStockLevel, opt => opt.MapFrom(src => src.MaxStockLevel))
            .ForMember(dest => dest.MinStockLevel, opt => opt.MapFrom(src => src.MinStockLevel))
            .ForMember(dest => dest.UnitOfMeasure, opt => opt.MapFrom(src => src.UnitOfMeasure))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReorderLevel, opt => opt.MapFrom(src => src.ReorderLevel))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorUserId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());

        CreateMap<MaterialEntity, MaterialDetailsViewModel>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.StockLevel, opt => opt.MapFrom(src => src.StockLevel))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.MaxStockLevel, opt => opt.MapFrom(src => src.MaxStockLevel))
            .ForMember(dest => dest.MinStockLevel, opt => opt.MapFrom(src => src.MinStockLevel))
            .ForMember(dest => dest.UnitOfMeasure, opt => opt.MapFrom(src => src.UnitOfMeasure))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReorderLevel, opt => opt.MapFrom(src => src.ReorderLevel))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.Id));

        CreateMap<MaterialEntity, MaterialDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.MaxStockLevel, opt => opt.MapFrom(src => src.MaxStockLevel))
            .ForMember(dest => dest.MinStockLevel, opt => opt.MapFrom(src => src.MinStockLevel))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ReorderLevel, opt => opt.MapFrom(src => src.ReorderLevel))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.StockLevel.StockId));
    }
}