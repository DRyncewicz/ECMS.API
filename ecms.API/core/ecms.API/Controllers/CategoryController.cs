using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.CreateCategory;
using ecms.Application.Handlers.Commands.DeleteCategory;
using ecms.Application.Handlers.Commands.DeleteProduct;
using ecms.Application.Handlers.Queries.GetAllCategoriesPaged;
using ecms.Application.Handlers.Queries.GetProductsByFilters;
using ecms.Application.Models.ViewModels.Categories;
using ecms.Application.Models.ViewModels.Products;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers;

public class CategoryController(IMediator _mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: categoryId => Created(string.Empty, categoryId),
            onFailure: CustomResults.Problem);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedCategoryViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] GetAllCategoriesPagedQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Result.Success(result).Match(
            onSuccess: categories => Ok(categories),
            onFailure: CustomResults.Problem);
    }

    [HttpDelete("{CategoryId}")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteAsync([FromRoute] int CategoryId, CancellationToken ct)
    {
        var command = new DeleteCategoryCommand(CategoryId);
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: isDeleted => Ok(isDeleted),
            onFailure: CustomResults.Problem);
    }
}
