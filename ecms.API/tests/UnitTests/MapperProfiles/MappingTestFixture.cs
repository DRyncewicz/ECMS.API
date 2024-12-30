using AutoMapper;
using ecms.Application.MapperProfiles.Categories;
using ecms.Application.MapperProfiles.Materials;
using ecms.Application.MapperProfiles.ProductMaterials;
using ecms.Application.MapperProfiles.Products;
using ecms.Application.MapperProfiles.StockLevels;
using ecms.Application.MapperProfiles.Stocks;
using ecms.Application.MapperProfiles.Suppliers;

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
            cfg.AddProfile<SupplierProfile>();
            cfg.AddProfile<ProductMaterialProfile>();
        });

        Mapper = ConfigurationProvider.CreateMapper();
    }
}