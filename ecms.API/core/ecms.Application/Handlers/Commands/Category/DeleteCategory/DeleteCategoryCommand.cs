using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.Category.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Result<string>>
{
    public int CategoryId { get; set; }

    public DeleteCategoryCommand(int categoryId)
    {
        CategoryId = categoryId;
    }
}