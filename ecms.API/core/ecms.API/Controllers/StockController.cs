using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.CreateStock;
using ecms.Application.Handlers.Commands.EditStock;
using ecms.Application.Handlers.Commands.DeleteCategory;
using ecms.Application.Handlers.Commands.DeleteStock;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers;

public class StockController(IMediator _mediator) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateStockCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: stockId => Created(string.Empty, stockId),
            onFailure: CustomResults.Problem);
    }

    [HttpPut("{StockId}")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditAsync([FromRoute] int StockId, [FromBody] EditStockRequest request, CancellationToken ct)
    {
        var command = new EditStockCommand(request, StockId);
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: StockId => NoContent(),
            onFailure: CustomResults.Problem);
    }

    [HttpDelete("{StockId}")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int StockId, CancellationToken ct)
    {
        var command = new DeleteStockCommand(StockId);
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: isDeleted => Ok(isDeleted),
            onFailure: CustomResults.Problem);
    }
}
