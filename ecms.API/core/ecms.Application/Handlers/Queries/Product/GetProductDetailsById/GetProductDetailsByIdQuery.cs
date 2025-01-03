using ecms.Application.Models.ViewModels.Products;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Queries.GetProductDetailsById;

public class GetProductDetailsByIdQuery : IRequest<Result<ProductDetailsViewModel>>
{
    public int ProductId { get; set; }

    public GetProductDetailsByIdQuery(int productId)
    {
        ProductId = productId;
    }
}