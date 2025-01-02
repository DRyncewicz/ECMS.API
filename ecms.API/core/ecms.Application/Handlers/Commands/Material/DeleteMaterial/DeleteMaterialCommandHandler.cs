using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.DeleteMaterial;

public class DeleteMaterialCommandHandler(IApplicationDbContext _applicationDbContext,
                                          IMapper _mapper,
                                          ICurrentUserService _userService,
                                          IDateTimeProvider _dateTimeProvider) : IRequestHandler<DeleteMaterialCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteMaterialCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync(ct);
        try
        {
            var material = _applicationDbContext.Materials.FirstOrDefault(p => p.Id == request.MaterialId);
            Ensure.NotNull(material);
            material.IsDeleted = true;
            _applicationDbContext.Materials.Update(material);
            await _applicationDbContext.SaveChangesAsync(ct);

            var materialHistory = _mapper.Map<MaterialHistoryEntity>(material);
            materialHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            materialHistory.CreatorUserId = _userService.UserId;
            await _applicationDbContext.MaterialHistories.AddAsync(materialHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var stockLevel = _applicationDbContext.StockLevels.FirstOrDefault(p => p.MaterialId == material.Id);
            Ensure.NotNull(stockLevel);
            stockLevel.IsDeleted = true;
            _applicationDbContext.StockLevels.Update(stockLevel);
            await _applicationDbContext.SaveChangesAsync(ct);

            transaction.Commit();
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}
