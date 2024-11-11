using AutoMapper;
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
    }
}
