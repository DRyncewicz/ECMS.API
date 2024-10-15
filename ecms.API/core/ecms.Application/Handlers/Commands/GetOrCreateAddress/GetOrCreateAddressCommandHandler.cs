using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.GetOrCreateAddress;

public class GetOrCreateAddressCommandHandler(IApplicationDbContext _applicationDbContext,
                                              IMapper _mapper) : IRequestHandler<GetOrCreateAddressCommand, Result<int>>
{
    public async Task<Result<int>> Handle(GetOrCreateAddressCommand request, CancellationToken ct)
    {
        var existingAddress = _applicationDbContext.Addresses.FirstOrDefault(p => p.Country == request.Country
                                                                                  && p.City == request.City
                                                                                  && p.Street == request.Street
                                                                                  && p.PostalCode == request.PostalCode
                                                                                  && p.BuildingNumber == request.BuildingNumber
                                                                                  && p.ApartmentNumber == request.ApartmentNumber);
        if (existingAddress != null)
        {
            return Result.Success(existingAddress.Id);
        }
        else
        {
            var newAddress = _mapper.Map<AddressEntity>(request);
                       
            await _applicationDbContext.Addresses.AddAsync(newAddress, ct);
            await _applicationDbContext.SaveChangesAsync(ct);
            
            return Result.Success(newAddress.Id);
        }
    }
}
