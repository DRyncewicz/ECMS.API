using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.CreateSupplier;
using ecms.Application.Handlers.Queries.GetSupplierDetailsById;
using ecms.Application.Models.ViewModels.Suppliers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using UnitTests.Handlers.Queries.GetSuppliersPaged;

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
        return Result.Success(result).Match(
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
        return Result.Success(result).Match(
            onSuccess: supplier => Ok(supplier),
            onFailure: CustomResults.Problem);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedSupplierViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] GetSuppliersPagedQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Result.Success(result).Match(
            onSuccess: suppliers => Ok(suppliers),
            onFailure: CustomResults.Problem);
    }
}