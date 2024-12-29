using ecms.Application.Models.Dtos.Materials;

namespace ecms.Application.Handlers.Commands.LinkProductVariantMaterials;

public class LinkProductVariantMaterialsRequest
{
    public List<ProductMaterialDto> ProductMaterialDtos { get; set; } = [];
}