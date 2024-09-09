using Asp.Versioning;
using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Queries.GetProductsByFilters;
using ecms.Application.Models.ViewModels.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers;

[ApiVersion(EcmsApiVersion.Version1)]
public class ProductController(IMediator _mediator) : BaseController
{
    [HttpGet("by-filters")]
    [ProducesResponseType(typeof(Result<FilteredProductsViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByFilters([FromQuery] GetProductsByFiltersQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Result.Success(result).Match(
            onSuccess: forecasts => Ok(forecasts),
            onFailure: CustomResults.Problem);
    }
}
