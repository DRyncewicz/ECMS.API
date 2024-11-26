using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.EditMaterial;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.EditMaterial;

public class EditMaterialCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly EditMaterialCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;

    public EditMaterialCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new EditMaterialCommandHandler(_applicationDbContext.Object, _mapper, _dateTimeProvider.Object, _userService.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        _userService.Setup(p => p.UserId).Returns("TestUserId");
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2024, 09, 22));
        List<MaterialEntity> materials = new List<MaterialEntity>()
        {
            new()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",                
            }
        };
        List<StockLevelEntity> stockLevels = new List<StockLevelEntity>()
        {
            new()
            {
                MaterialId = 1,
                BatchNumber = "Batch",
                StockId = 1,
            }
        };
        _applicationDbContext.Setup(p => p.Materials).Returns(materials.AsQueryable().BuildMock().Object);
        _applicationDbContext.Setup(p => p.StockLevels).Returns(stockLevels.AsQueryable().BuildMock().Object);
    }

    [Fact]
    public async Task Handle_ShouldEditMaterial()
    {
        //Arrange
        var request = new EditMaterialCommand()
        {
            MaterialId = 1,
            Name = "NewName",
            Description = "NewDescription",
            StockId = 1,
            BatchNumber = "Batch",
        };
        _applicationDbContext.Setup(p => p.MaterialHistories).Returns(new Mock<DbSet<MaterialHistoryEntity>>().Object);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _applicationDbContext.Verify(p => p.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        _applicationDbContext.Verify(p => p.MaterialHistories.AddAsync(It.IsAny<MaterialHistoryEntity>(), It.IsAny<CancellationToken>()), Times.Once());
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldEditMaterial_And_StockLevel()
    {
        //Arrange
        var request = new EditMaterialCommand()
        {
            MaterialId = 1,
            Name = "NewName",
            Description = "NewDescription",
            StockId = 1,
            BatchNumber = "NewBatch",
        };
        _applicationDbContext.Setup(p => p.MaterialHistories).Returns(new Mock<DbSet<MaterialHistoryEntity>>().Object);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _applicationDbContext.Verify(p => p.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(3));
        _applicationDbContext.Verify(p => p.MaterialHistories.AddAsync(It.IsAny<MaterialHistoryEntity>(), It.IsAny<CancellationToken>()), Times.Once());
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollBackTransaction_WhenExceptionOccurs()
    {
        // Arrange
        var request = new EditMaterialCommand();

        _applicationDbContext.Setup(p => p.MaterialHistories.AddAsync(It.IsAny<MaterialHistoryEntity>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        _transaction.Verify(p => p.Rollback(), Times.Once);
    }
}
