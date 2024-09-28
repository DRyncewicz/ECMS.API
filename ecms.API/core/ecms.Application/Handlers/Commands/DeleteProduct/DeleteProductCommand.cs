using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Result<bool>>
{
    public int ProductId { get; set; }

    public DeleteProductCommand(int productId)
    {
        ProductId = productId;
    }
}
