using AutoMapper;
using ecms.Application.Handlers.Commands.CreateCategory;
using ecms.Domain.Entities;

namespace ecms.Application.MapperProfiles.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryCommand, CategoryEntity>()
            .ForMember(dest => dest.FileGuid, opt => opt.MapFrom(src => src.FileGuid))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.HierarchyId, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}
