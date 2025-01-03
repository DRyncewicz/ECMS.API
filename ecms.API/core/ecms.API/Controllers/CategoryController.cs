using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.Category.CreateCategory;
using ecms.Application.Handlers.Commands.Category.DeleteCategory;
using ecms.Application.Handlers.Commands.Category.EditCategory;
using ecms.Application.Handlers.Queries.Category.GetAllCategoriesPaged;
using ecms.Application.Handlers.Queries.Category.GetCategoryById;
using ecms.Application.Models.ViewModels.Categories;
using MediatR;
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
        return result.Match(
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
        return result.Match(
            onSuccess: categories => Ok(categories),
            onFailure: CustomResults.Problem);
    }

    [HttpGet("{CategoryId}")]
    [ProducesResponseType(typeof(Result<CategoryViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int CategoryId, CancellationToken ct)
    {
        var query = new GetCategoryByIdQuery(CategoryId);
        var result = await _mediator.Send(query, ct);
        return result.Match(
            onSuccess: category => Ok(category),
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
        return result.Match(
            onSuccess: isDeleted => Ok(isDeleted),
            onFailure: CustomResults.Problem);
    }

    [HttpPut("{CategoryId}")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditAsync([FromRoute] int CategoryId, [FromBody] EditCategoryRequest request, CancellationToken ct)
    {
        var command = new EditCategoryCommand(request, CategoryId);
        var result = await _mediator.Send(command, ct);
        return result.Match(
            onSuccess: CategoryId => NoContent(),
            onFailure: CustomResults.Problem);
    }
}