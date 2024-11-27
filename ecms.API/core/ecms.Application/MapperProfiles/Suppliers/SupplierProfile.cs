using AutoMapper;
using ecms.Application.Handlers.Commands.CreateSupplier;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.Suppliers;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<CreateSupplierCommand, SupplierEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
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
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
