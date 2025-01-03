using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.Product.DeleteProduct;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.Product.DeleteProduct;

public class DeleteProductCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly DeleteProductCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;

    private readonly List<ProductEntity> _products = new List<ProductEntity>
        {
            new ProductEntity
            {
                Id = 1,
                Name = "Test1",
                CategoryId = 1
            },
            new ProductEntity
            {
                Id = 2,
                Name = "Test2",
                CategoryId = 1
            },
            new ProductEntity
            {
                Id = 3,
                Name = "Test3",
                CategoryId = 2
            },
            new ProductEntity
            {
                Id = 4,
                Name = "Test4",
                CategoryId = 2
            }
        };

    private readonly List<ProductVariantEntity> _productVariants = new List<ProductVariantEntity>
        {
            new ProductVariantEntity
            {
                Id = 1,
                Name = "Test1",
                ProductId = 1
            },
            new ProductVariantEntity
            {
                Id = 2,
                Name = "Test2",
                ProductId = 1
            },
        };

    public DeleteProductCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new DeleteProductCommandHandler(_applicationDbContext.Object, _mapper, _userService.Object, _dateTimeProvider.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        var dbContextResponseProducts = _products.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Products).Returns(dbContextResponseProducts.Object);
        var dbContextResponseVariants = _productVariants.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.ProductVariants).Returns(dbContextResponseVariants.Object);
        _userService.Setup(p => p.UserId).Returns("TestUserId");
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2024, 09, 22));
    }

    [Fact]
    public async Task Handle_ShouldDeleteProduct()
    {
        //Arrange
        var request = new DeleteProductCommand(1);
        _applicationDbContext.Setup(p => p.ProductHistories.AddAsync(It.IsAny<ProductHistoryEntity>(), It.IsAny<CancellationToken>())).Returns(new ValueTask<EntityEntry<ProductHistoryEntity>>());
        _applicationDbContext.Setup(p => p.ProductVariantHistories.AddRangeAsync(It.IsAny<IEnumerable<ProductVariantHistoryEntity>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ValueTask<EntityEntry<ProductVariantHistoryEntity>>()));

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().Be(true);
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollBackTransaction_WhenExceptionOccurs()
    {
        // Arrange
        var request = new DeleteProductCommand(1);

        _applicationDbContext.Setup(p => p.ProductHistories.AddAsync(It.IsAny<ProductHistoryEntity>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception("Simulated exception"));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        _transaction.Verify(p => p.Rollback(), Times.Once);
    }
}