using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.EditProduct;
using ecms.Application.Models.Dtos.Products;
using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.EditProduct;

public class EditProductCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly EditProductCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;
    private readonly List<ProductVariantEntity> _variants;
    public EditProductCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new EditProductCommandHandler(_applicationDbContext.Object, _mapper, _userService.Object, _dateTimeProvider.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        _userService.Setup(p => p.UserId).Returns("TestUserId");
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2024, 09, 22));
        _variants = new List<ProductVariantEntity>()
        {
            new ProductVariantEntity()
            {
                Id = 1,
                IsDeleted = false,
                Name = "Test1",
                Price = new ecms.Domain.ValueObjects.Price(1, ecms.Domain.ValueObjects.Currency.Pln),
                ProductId = 1
            }
        };
        _applicationDbContext.Setup(p => p.ProductVariants).Returns(_variants.AsQueryable().BuildMock().Object);
    }

    [Fact]
    public async Task Handle_ShouldEditProductAndAddProductVariant_OnValidRequest()
    {
        //Arrange
        var request = new EditProductCommand()
        {
            ProductVariants = new List<ProductVariantDto>()
            {
                new ProductVariantDto()
                {
                    Id = 0,
                    ProductId = 1,
                    Name = "Test",
                    Price = new ecms.Domain.ValueObjects.Price(1, ecms.Domain.ValueObjects.Currency.Pln)
                },
                new()
                {
                Id = 1,
                Name = "Test1",
                Price = new ecms.Domain.ValueObjects.Price(1, ecms.Domain.ValueObjects.Currency.Pln),
                ProductId = 1
                }
            }
        };
        _applicationDbContext.Setup(p => p.Products).Returns(new Mock<DbSet<ProductEntity>>().Object);
        _applicationDbContext.Setup(p => p.ProductHistories.AddAsync(It.IsAny<ProductHistoryEntity>(), It.IsAny<CancellationToken>())).Returns(new ValueTask<EntityEntry<ProductHistoryEntity>>());
        _applicationDbContext.Setup(p => p.ProductVariantHistories.AddRangeAsync(It.IsAny<IEnumerable<ProductVariantHistoryEntity>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new ValueTask<EntityEntry<ProductVariantHistoryEntity>>()));

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _applicationDbContext.Verify(p => p.Products.Update(It.IsAny<ProductEntity>()), Times.Once());
        _applicationDbContext.Verify(p => p.ProductVariants.AddRangeAsync(It.IsAny<IEnumerable<ProductVariantEntity>>(), It.IsAny<CancellationToken>()), Times.Once());
        _applicationDbContext.Verify(p => p.ProductVariantHistories.AddRangeAsync(It.IsAny<IEnumerable<ProductVariantHistoryEntity>>(), It.IsAny<CancellationToken>()), Times.Once());
        _transaction.Verify(p => p.Commit(), Times.Once);
    }
}
