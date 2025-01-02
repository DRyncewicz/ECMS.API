using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernal;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.Material.EditMaterial;

public class EditMaterialCommandHandler(IApplicationDbContext _applicationDbContext,
                                        IMapper _mapper,
                                        IDateTimeProvider _dateTimeProvider,
                                        ICurrentUserService _userService) : IRequestHandler<EditMaterialCommand, Result<int>>
{
    public async Task<Result<int>> Handle(EditMaterialCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync();
        try
        {
            var materialToEdit = _applicationDbContext.Materials.FirstOrDefault(p => p.Id == request.MaterialId);
            _mapper.Map(request, materialToEdit);
            Ensure.NotNull(materialToEdit);
            await _applicationDbContext.SaveChangesAsync(ct);

            var materialHistory = _mapper.Map<MaterialHistoryEntity>(materialToEdit);
            materialHistory.CreateDateTimeUtc = _dateTimeProvider.UtcNow;
            materialHistory.CreatorUserId = _userService.UserId;
            await _applicationDbContext.MaterialHistories.AddAsync(materialHistory, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            var stockLevel = _applicationDbContext.StockLevels.FirstOrDefault(p => p.MaterialId == materialToEdit.Id);
            Ensure.NotNull(stockLevel);
            if (stockLevel.BatchNumber != request.BatchNumber || stockLevel.StockId != request.StockId)
            {
                _mapper.Map(request, stockLevel);
                await _applicationDbContext.SaveChangesAsync(ct);
            }

            transaction.Commit();
            return Result.Success(materialToEdit.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}
