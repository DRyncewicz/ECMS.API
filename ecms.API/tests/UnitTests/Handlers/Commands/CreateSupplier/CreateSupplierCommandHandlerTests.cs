using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.CreateSupplier;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.CreateSupplier;

public class CreateSupplierCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly CreateSupplierCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;
    public CreateSupplierCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new CreateSupplierCommandHandler(_applicationDbContext.Object, _mapper, _dateTimeProvider.Object, _userService.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        _userService.Setup(p => p.UserId).Returns("TestUserId");
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2024, 09, 22));
    }

    [Fact]
    public async Task Handle_ShouldCreateSupplier()
    {
        //Arrange
        var request = new CreateSupplierCommand();
        _applicationDbContext.Setup(p => p.Suppliers).Returns(new Mock<DbSet<SupplierEntity>>().Object);
        _applicationDbContext.Setup(p => p.SupplierHistories).Returns(new Mock<DbSet<SupplierHistoryEntity>>().Object);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollBackTransaction_WhenExceptionOccurs()
    {
        // Arrange
        var request = new CreateSupplierCommand();

        _applicationDbContext.Setup(p => p.SupplierHistories.AddAsync(It.IsAny<SupplierHistoryEntity>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        _transaction.Verify(p => p.Rollback(), Times.Once);
    }
}
