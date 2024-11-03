using Asp.Versioning;
using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.DeleteProduct;
using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Application.Handlers.Queries.GetProductDetailsById;
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
    public async Task<IActionResult> GetByFiltersAsync([FromQuery] GetProductsByFiltersQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);
        return Result.Success(result).Match(
            onSuccess: products => Ok(products),
            onFailure: CustomResults.Problem);
    }

    [HttpGet("{ProductId}")]
    [ProducesResponseType(typeof(Result<ProductDetailsViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailsByIdAsync([FromRoute] int ProductId, CancellationToken ct)
    {
        var query = new GetProductDetailsByIdQuery(ProductId);
        var result = await _mediator.Send(query, ct);
        return Result.Success(result).Match(
            onSuccess: product => Ok(product),
            onFailure: CustomResults.Problem);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: productId => Created(string.Empty, productId),
            onFailure: CustomResults.Problem);
    }

    [HttpDelete("{ProductId}")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int ProductId, CancellationToken ct)
    {
        var command = new DeleteProductCommand(ProductId);
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: isDeleted => NoContent(),
            onFailure: CustomResults.Problem);
    }

    [HttpPut("{ProductId}")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditAsync([FromRoute] int ProductId, [FromBody] EditProductRequest request, CancellationToken ct)
    {
        var command = new EditProductCommand(request, ProductId);
        var result = await _mediator.Send(command, ct);
        return Result.Success(result).Match(
            onSuccess: ProductId => NoContent(),
            onFailure: CustomResults.Problem);
    }
}