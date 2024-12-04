using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.CreateSupplier;
using ecms.Application.Handlers.Commands.DeleteSupplier;
using ecms.Application.Handlers.Queries.GetSupplierDetailsById;
using ecms.Application.Models.ViewModels.Suppliers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers;

public class SupplierController(IMediator _mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateSupplierCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return result.Match(
            onSuccess: supplierId => Created(string.Empty, supplierId),
            onFailure: CustomResults.Problem);
    }

    [HttpGet("{SupplierId}")]
    [ProducesResponseType(typeof(Result<SupplierDetailsViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailsByIdAsync([FromRoute] int SupplierId, CancellationToken ct)
    {
        var query = new GetSupplierDetailsByIdQuery(SupplierId);
        var result = await _mediator.Send(query, ct);
        return result.Match(
            onSuccess: supplier => Ok(supplier),
            onFailure: CustomResults.Problem);
    }

    [HttpDelete("{SupplierId}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int SupplierId, CancellationToken ct)
    {
        var command = new DeleteSupplierCommand(SupplierId);
        var result = await _mediator.Send(command, ct);
        return result.Match(
            onSuccess: isDeleted => Ok(isDeleted),
            onFailure: CustomResults.Problem);
    }
}