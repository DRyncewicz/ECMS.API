using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Domain.Entities;
using MediatR;
using SharedKernel;

namespace ecms.Application.Handlers.Commands.CreateCategory;

public class CreateCategoryCommandHandler(IApplicationDbContext _applicationDbContext,
                                          IMapper _mapper) : IRequestHandler<CreateCategoryCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken ct)
    {
        using var transaction = await _applicationDbContext.BeginTransactionAsync();
        try
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(request);
            var children = _applicationDbContext.Categories.Where(p => p.HierarchyId.IsDescendantOf(request.AncenstorHierarchyId) && p.HierarchyId.GetLevel() == request.AncenstorHierarchyId.GetLevel() + 1);

            if (children.Any())
            {
                var highestChild = children.OrderBy(p => p.HierarchyId).Last();
                categoryEntity.HierarchyId = request.AncenstorHierarchyId.GetDescendant(highestChild.HierarchyId);
            }
            else
            {
                categoryEntity.HierarchyId = request.AncenstorHierarchyId.GetDescendant(null);
            }

            await _applicationDbContext.Categories.AddAsync(categoryEntity, ct);
            await _applicationDbContext.SaveChangesAsync(ct);

            transaction.Commit();
            return Result.Success(categoryEntity.Id);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }
}

