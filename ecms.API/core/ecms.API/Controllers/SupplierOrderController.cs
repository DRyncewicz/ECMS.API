using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Handlers.Queries.SupplierOrder.GetSupplierOrderDetailsById;
using ecms.Application.Handlers.Queries.SupplierOrder.GetSupplierOrdersPaged;
using ecms.Application.Models.ViewModels.SupplierOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers;

public class SupplierOrderController(IMediator _mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSupplierOrderAsync([FromBody] CreateSupplierOrderCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return result.Match(
            onSuccess: supplierOrderId => Created(string.Empty, supplierOrderId),
            onFailure: CustomResults.Problem);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<SupplierOrdersViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] GetSupplierOrdersPagedQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Result.Success(result).Match(
            onSuccess: supplierOrders => Ok(supplierOrders),
            onFailure: CustomResults.Problem);
    }

    [HttpGet("{SupplierOrderId}")]
    [ProducesResponseType(typeof(Result<SupplierOrderDetailsViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailsByIdAsync([FromRoute] int SupplierOrderId, CancellationToken ct)
    {
        var query = new GetSupplierOrderDetailsByIdQuery(SupplierOrderId);
        var result = await _mediator.Send(query, ct);
        return result.Match(
            onSuccess: supplierOrder => Ok(supplierOrder),
            onFailure: CustomResults.Problem);
    }
}