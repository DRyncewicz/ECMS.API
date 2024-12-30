using ecms.Application.Models.Dtos.Materials;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.LinkProductVariantMaterials;

public class LinkProductVariantMaterialsCommand : IRequest<Result<bool>>
{
    public int ProductVariantId { get; set; }

    public List<ProductMaterialDto> ProductMaterialDtos { get; set; } = [];

    public LinkProductVariantMaterialsCommand(LinkProductVariantMaterialsRequest request, int productVariantId)
    {
        ProductVariantId = productVariantId;
        ProductMaterialDtos = request.ProductMaterialDtos;
    }

    public LinkProductVariantMaterialsCommand()
    {
    }
}