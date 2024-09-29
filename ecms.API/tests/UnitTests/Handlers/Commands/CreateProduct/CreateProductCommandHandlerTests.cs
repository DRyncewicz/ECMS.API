using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.DeleteProduct;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.CreateProduct;

public class CreateProductCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly CreateProductCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;

    public CreateProductCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new CreateProductCommandHandler(_applicationDbContext.Object, _mapper, _userService.Object, _dateTimeProvider.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        _userService.Setup(p => p.UserId).Returns("TestUserId");
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2024, 09, 22));
    }

    [Fact]
    public async Task Handle_ShouldCreateProduct()
    {
        //Arrange
        var request = new CreateProductCommand();
        _applicationDbContext.Setup(p => p.Products.AddAsync(It.IsAny<ProductEntity>(), It.IsAny<CancellationToken>())).Returns(new ValueTask<EntityEntry<ProductEntity>>());
        _applicationDbContext.Setup(p => p.ProductVariants.AddRangeAsync(It.IsAny<IEnumerable<ProductVariantEntity>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ValueTask<EntityEntry<ProductVariantEntity>>()));
        _applicationDbContext.Setup(p => p.ProductHistories.AddAsync(It.IsAny<ProductHistoryEntity>(), It.IsAny<CancellationToken>())).Returns(new ValueTask<EntityEntry<ProductHistoryEntity>>());
        _applicationDbContext.Setup(p => p.ProductVariantHistories.AddRangeAsync(It.IsAny<IEnumerable<ProductVariantHistoryEntity>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ValueTask<EntityEntry<ProductVariantHistoryEntity>>()));

        //Act
        var result = await _handler.Handle(request, default);

        //Assert        
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollBackTransaction_WhenExceptionOccurs()
    {
        // Arrange
        var request = new CreateProductCommand();

        _applicationDbContext.Setup(p => p.ProductHistories.AddAsync(It.IsAny<ProductHistoryEntity>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        _transaction.Verify(p => p.Rollback(), Times.Once);
    }
}
