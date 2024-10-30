using AutoMapper;
using ecms.Application.Handlers.Commands.GetOrCreateAddress;
using ecms.Application.Models.Dtos.Addresses;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.Categories;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<GetOrCreateAddressCommand, AddressEntity>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
            .ForMember(dest => dest.ApartmentNumber, opt => opt.MapFrom(src => src.ApartmentNumber))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Stocks, opt => opt.Ignore())
            .ForMember(dest => dest.Suppliers, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());

        CreateMap<AddressEntity, AddressDto>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
            .ForMember(dest => dest.ApartmentNumber, opt => opt.MapFrom(src => src.ApartmentNumber))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.Id));            
    }
}