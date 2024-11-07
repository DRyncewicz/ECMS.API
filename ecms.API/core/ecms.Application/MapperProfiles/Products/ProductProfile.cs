using AutoMapper;
using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Application.Models.Dtos.Allergens;
using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductVariantEntity, ProductVariantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Allergens, opt => opt.Ignore());

        CreateMap<AllergenEntity, AllergenDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AllergenId, opt => opt.MapFrom(src => src.Id));

        CreateMap<ProductEntity, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.Vat));

        CreateMap<EditProductCommand, ProductEntity>()
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.AlcoholContent, opt => opt.MapFrom(src => src.AlcoholContent))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
            .ForMember(dest => dest.GtuCode, opt => opt.MapFrom(src => src.GtuCode))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.Vat))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.ProductVariants, opt => opt.Ignore())
            .ForMember(dest => dest.ProductHistories, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.ProductMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());

        CreateMap<ProductVariantDto, ProductVariantEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.ProductVariantHistories, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.OrderProductVariants, opt => opt.Ignore())
            .ForMember(dest => dest.ProductVariantAllergens, opt => opt.Ignore())
            .ForMember(dest => dest.ProductMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<CreateProductCommand, ProductEntity>()
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.AlcoholContent, opt => opt.MapFrom(src => src.AlcoholContent))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
            .ForMember(dest => dest.GtuCode, opt => opt.MapFrom(src => src.GtuCode))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.Vat))
            .ForMember(dest => dest.ProductVariants, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProductHistories, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.ProductMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());

        CreateMap<CreateProductVariantDto, ProductVariantEntity>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.ProductVariantHistories, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.OrderProductVariants, opt => opt.Ignore())
            .ForMember(dest => dest.ProductVariantAllergens, opt => opt.Ignore())
            .ForMember(dest => dest.ProductMaterials, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        CreateMap<ProductEntity, ProductHistoryEntity>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.AlcoholContent, opt => opt.MapFrom(src => src.AlcoholContent))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
            .ForMember(dest => dest.GtuCode, opt => opt.MapFrom(src => src.GtuCode))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.Vat))
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorUserId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ProductVariantEntity, ProductVariantHistoryEntity>()
            .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorUserId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductVariant, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}