using AutoMapper;
using ecms.Application.Handlers.Commands.Material.CreateMaterial;
using ecms.Application.Handlers.Commands.Material.EditMaterial;
using ecms.Application.Models.Dtos.StockLevels;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.StockLevels;

public class StockLevelProfile : Profile
{
    public StockLevelProfile()
    {
        CreateMap<StockLevelEntity, StockLevelDto>()
            .ForMember(dest => dest.StockLevelId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.StockId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.BatchNumber, opt => opt.MapFrom(src => src.BatchNumber))
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated));

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

        CreateMap<EditMaterialCommand, StockLevelEntity>()
            .ForMember(dest => dest.StockId, opt => opt.MapFrom(src => src.StockId))
            .ForMember(dest => dest.BatchNumber, opt => opt.MapFrom(src => src.BatchNumber))
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Quantity, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreateDateTimeUtc, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.StockTransactions, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdated, opt => opt.Ignore())
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
