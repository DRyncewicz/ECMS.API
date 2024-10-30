using AutoMapper;
using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.CreateStock;
using ecms.Application.Handlers.Commands.EditStock;
using ecms.Application.Models.Dtos.Stocks;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.Stocks;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<CreateStockCommand, StockEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.StockLevels, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<EditStockCommand, StockEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StockId))
            .ForMember(dest => dest.Address, opt => opt.Ignore())
            .ForMember(dest => dest.StockLevels, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<StockEntity, StockDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AddressDto, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.Id));

    }
}
