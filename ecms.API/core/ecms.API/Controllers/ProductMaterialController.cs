using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Queries.Product.GetGroupProductMaterialsByFilters;
using ecms.Application.Models.ViewModels.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers;

public class ProductMaterialController(IMediator _mediator) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(Result<GroupProductMaterialViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGroupProductMaterialsAsync([FromQuery] GetGroupProductMaterialsByFiltersQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return result.Match(
            onSuccess: productMaterials => Ok(productMaterials),
            onFailure: CustomResults.Problem);
    }
}