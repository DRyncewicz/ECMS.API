using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.GetOrCreateAddress;

public class GetOrCreateAddressCommand : IRequest<Result<int>>
{
    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string BuildingNumber { get; set; } = string.Empty;

    public string ApartmentNumber { get; set; } = string.Empty;
}
