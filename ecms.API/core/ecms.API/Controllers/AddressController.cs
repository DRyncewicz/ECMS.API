using ecms.API.Controllers.Base;
using ecms.API.Extensions;
using ecms.API.Infrastructure;
using ecms.Application.Handlers.Commands.GetOrCreateAddress;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ecms.API.Controllers
{
    public class AddressController(IMediator _mediator) : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAsync([FromBody] GetOrCreateAddressCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return Result.Success(result).Match(
                onSuccess: addressId => Ok(addressId),
                onFailure: CustomResults.Problem);
        }
    }
}