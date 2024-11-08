using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.DeleteMaterial;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.DeleteMaterial;

public class DeleteMaterialCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly DeleteMaterialCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;
    private readonly List<MaterialEntity> _materials = new List<MaterialEntity>
    {
        new()
        {
            Id = 1,
        },
        new()
        {
            Id = 2,
        }
    };
    private readonly List<StockLevelEntity> _stockLevels = new List<StockLevelEntity>
    {
        new()
        {
            Id = 1,
            MaterialId = 1,
        },
        new()
        {
            Id = 2,
            MaterialId = 1,
        }
    };

    public DeleteMaterialCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new DeleteMaterialCommandHandler(_applicationDbContext.Object, _mapper, _userService.Object, _dateTimeProvider.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        var dbContextResponseMaterials = _materials.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Materials).Returns(dbContextResponseMaterials.Object);
        var dbContextResponseStockLevels = _stockLevels.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.StockLevels).Returns(dbContextResponseStockLevels.Object);
        _userService.Setup(p => p.UserId).Returns("TestUserId");
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2024, 09, 22));
    }

    [Fact]
    public async Task Handle_ShouldDeleteMaterial()
    {
        //Arrange
        var request = new DeleteMaterialCommand(1);
        _applicationDbContext.Setup(p => p.MaterialHistories).Returns(new Mock<DbSet<MaterialHistoryEntity>>().Object);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().Be(true);
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollBackTransaction_WhenExceptionOccurs()
    {
        //Arrange
        var request = new DeleteMaterialCommand(1);
        _applicationDbContext.Setup(p => p.MaterialHistories.AddAsync(It.IsAny<MaterialHistoryEntity>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception());

        //Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        //Assert
        await act.Should().ThrowAsync<Exception>();
        _transaction.Verify(p => p.Rollback(), Times.Once);
    }
}
