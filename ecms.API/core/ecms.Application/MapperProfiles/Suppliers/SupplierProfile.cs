using AutoMapper;
using ecms.Application.Handlers.Commands.CreateSupplier;
using ecms.Application.Models.Dtos.Suppliers;
using ecms.Application.Models.ViewModels.Suppliers;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.Suppliers;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<CreateSupplierCommand, SupplierEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierContacts, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierHistories, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierOrders, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<SupplierEntity, SupplierHistoryEntity>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.CreatorUserId, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<SupplierEntity, SupplierDetailsViewModel>()
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ContactDtos, opt => opt.MapFrom(src => src.SupplierContacts))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<SupplierContactEntity, SupplierContactDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.IsCommon, opt => opt.MapFrom(src => src.IsCommon))
            .ForMember(dest => dest.RepresentativeName, opt => opt.MapFrom(src => src.RepresentativeName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));



    }
}
