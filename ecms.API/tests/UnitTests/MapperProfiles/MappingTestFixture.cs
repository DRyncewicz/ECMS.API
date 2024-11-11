using AutoMapper;
using ecms.Application.MapperProfiles.Categories;
using ecms.Application.MapperProfiles.Materials;
using ecms.Application.MapperProfiles.Products;
using ecms.Application.MapperProfiles.StockLevels;
using ecms.Application.MapperProfiles.Stocks;

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
            cfg.AddProfile<CategoryProfile>();
            cfg.AddProfile<AddressProfile>();
            cfg.AddProfile<StockProfile>();
            cfg.AddProfile<MaterialProfile>();
            cfg.AddProfile<StockLevelProfile>();
        });

        Mapper = ConfigurationProvider.CreateMapper();
    }
}