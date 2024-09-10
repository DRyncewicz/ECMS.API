using AutoMapper;
using ecms.Application.MapperProfiles.Products;

namespace UnitTests.Mapping;

public class MappingTestFixture
{
    public IConfigurationProvider ConfigurationProvider { get; set; }

    public IMapper Mapper { get; set; }

    public MappingTestFixture()
    {
        ConfigurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductProfile>();
        });

        Mapper = ConfigurationProvider.CreateMapper();
    }
}