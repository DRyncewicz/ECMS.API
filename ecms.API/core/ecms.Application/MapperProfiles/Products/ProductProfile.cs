using AutoMapper;
using ecms.Application.Handlers.Commands.CreateProduct;
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
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

        CreateMap<ProductEntity, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.Vat));

        CreateMap<CreateProductCommand, ProductEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.AlcoholContent, opt => opt.MapFrom(src => src.AlcoholContent))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
            .ForMember(dest => dest.GtuCode, opt => opt.MapFrom(src => src.GtuCode))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.Vat));

        CreateMap<CreateProductVariantDto, ProductVariantEntity>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
    }
}
