using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateMaterial;

public class CreateMaterialCommandHandler(IApplicationDbContext _applicationDbContext,
                                          IMapper _mapper,
                                          IDateTimeProvider _dateTimeProvider,
                                          ICurrentUserService _userService) : IRequestHandler<CreateMaterialCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateMaterialCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync(ct);
        try
        {
            var materialEntity = _mapper.Map<MaterialEntity>(request);
            await _applicationDbContext.Materials.AddAsync(materialEntity, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var materialHistory = _mapper.Map<MaterialHistoryEntity>(materialEntity);
            materialHistory.CreatorUserId = _userService.UserId;
            materialHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            await _applicationDbContext.MaterialHistories.AddAsync(materialHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var stockLevel = _mapper.Map<StockLevelEntity>(request);
            stockLevel.MaterialId = materialEntity.Id;
            stockLevel.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            await _applicationDbContext.StockLevels.AddAsync(stockLevel, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            transaction.Commit();
            return Result.Success(materialEntity.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}
