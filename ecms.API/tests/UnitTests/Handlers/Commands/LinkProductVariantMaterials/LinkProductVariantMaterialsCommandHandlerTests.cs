using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.LinkProductVariantMaterials;
using ecms.Application.Models.Dtos.Materials;
using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.LinkProductVariantMaterials;

public class LinkProductVariantMaterialsCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly LinkProductVariantMaterialsCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;
    private readonly List<ProductVariantEntity> _variants;
    private readonly List<ProductMaterialEntity> _productMaterials;

    public LinkProductVariantMaterialsCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new LinkProductVariantMaterialsCommandHandler(_applicationDbContext.Object, _mapper, _dateTimeProvider.Object, _userService.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        _userService.Setup(p => p.UserId).Returns("TestUserId");
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2024, 09, 22));
        _productMaterials = new List<ProductMaterialEntity>()
        {
            new()
            {
                MaterialId = 1,
                Quantity = 5,
                ProductVariantId = 1
            },
            new()
            {
                MaterialId = 2,
                Quantity = 10,
                ProductVariantId = 1
            }
        };
        _variants = new List<ProductVariantEntity>()
        {
            new ProductVariantEntity()
            {
                Id = 1,
                IsDeleted = false,
            }
        };
        _applicationDbContext.Setup(p => p.ProductVariants).Returns(_variants.AsQueryable().BuildMock().Object);
        _applicationDbContext.Setup(p => p.ProductMaterials).Returns(_productMaterials.AsQueryable().BuildMock().Object);
        _applicationDbContext.Setup(p => p.ProductMaterialHistories).Returns(new Mock<DbSet<ProductMaterialHistoryEntity>>().Object);
    }

    [Theory]
    [InlineData(new int[] { 3 }, 1, 1, 3, 2)]
    [InlineData(new int[] { 2, 3 }, 1, 2, 4, 3)]
    [InlineData(new int[] { 2 }, 0, 2, 2, 2)]
    [InlineData(new int[] { 1, 2 }, 0, 1, 1, 1)]
    [InlineData(new int[] { }, 0, 1, 1, 1)]
    public async Task Handle_ShouldLinkProductVariantMaterials(int[] materialIds, int productMaterialsAddRangeCount, int productMaterialsUpdateRangeCount, int saveChangesCount, int productMaterialHistoriesAddCount)
    {
        // Arrange
        var productMaterialDtos = new List<ProductMaterialDto>();
        foreach (var materialId in materialIds)
        {
            var productMaterialDto = new ProductMaterialDto()
            {
                MaterialId = materialId,
            };
            productMaterialDtos.Add(productMaterialDto);
        }
        var command = new LinkProductVariantMaterialsCommand
        {
            ProductVariantId = 1,
            ProductMaterialDtos = productMaterialDtos
        };

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        _applicationDbContext.Verify(p => p.ProductMaterialHistories.AddRangeAsync(It.IsAny<List<ProductMaterialHistoryEntity>>(), It.IsAny<CancellationToken>()), Times.Exactly(productMaterialHistoriesAddCount));
        _applicationDbContext.Verify(p => p.ProductMaterials.AddRange(It.IsAny<List<ProductMaterialEntity>>()), Times.Exactly(productMaterialsAddRangeCount));
        _applicationDbContext.Verify(p => p.ProductMaterials.UpdateRange(It.IsAny<List<ProductMaterialEntity>>()), Times.Exactly(productMaterialsUpdateRangeCount));
        _applicationDbContext.Verify(p => p.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(saveChangesCount));
        _transaction.Verify(p => p.Commit(), Times.Once());
    }
}