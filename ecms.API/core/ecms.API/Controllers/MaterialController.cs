using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.CreateMaterial;
using ecms.Application.Handlers.Commands.DeleteMaterial;
using ecms.Application.Handlers.Queries.GetMaterialDetailsById;
using ecms.Application.Handlers.Queries.GetMaterialsByFilters;
using ecms.Application.Handlers.Queries.GetProductDetailsById;
using ecms.Application.Handlers.Queries.GetProductsByFilters;
using ecms.Application.Models.ViewModels.Materials;
using ecms.Application.Models.ViewModels.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers;

public class MaterialController(IMediator _mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMaterialCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: materialId => Created(string.Empty, materialId),
            onFailure: CustomResults.Problem);
    }

    [HttpDelete("{MaterialId}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int MaterialId, CancellationToken ct)
    {
        var command = new DeleteMaterialCommand(MaterialId);
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: isDeleted => Ok(isDeleted),
            onFailure: CustomResults.Problem);
    }

    [HttpGet("{MaterialId}")]
    [ProducesResponseType(typeof(Result<MaterialDetailsViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailsByIdAsync([FromRoute] int MaterialId, CancellationToken ct)
    {
        var query = new GetMaterialDetailsByIdQuery(MaterialId);
        var result = await _mediator.Send(query, ct);
        return Result.Success(result).Match(
            onSuccess: material => Ok(material),
            onFailure: CustomResults.Problem);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<FilteredMaterialsViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByFiltersAsync([FromQuery] GetMaterialsByFiltersQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Result.Success(result).Match(
            onSuccess: materials => Ok(materials),
            onFailure: CustomResults.Problem);
    }
}
