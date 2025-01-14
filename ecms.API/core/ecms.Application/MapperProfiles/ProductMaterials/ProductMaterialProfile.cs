using AutoMapper;
using ecms.Application.Models.Dtos.Materials;
using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.ProductMaterials;

public class ProductMaterialProfile : Profile
{
    public ProductMaterialProfile()
    {
        CreateMap<ProductMaterialEntity, ProductMaterialDto>()
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<ProductMaterialDto, ProductMaterialEntity>()
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.ProductVariant, opt => opt.Ignore())
            .ForMember(dest => dest.ProductMaterialHistories, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProductVariantId, opt => opt.Ignore());

        CreateMap<ProductMaterialEntity, ProductMaterialHistoryEntity>()
            .ForMember(dest => dest.ProductMaterialId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.ProductVariantId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProductMaterial, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorUserId, opt => opt.Ignore());

        CreateMap<ProductMaterialEntity, ProductMaterialListItemDto>()
            .ForMember(dest => dest.ProductMaterialId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Material.Name));
    }
}