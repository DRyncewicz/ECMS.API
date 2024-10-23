using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.CreateProduct;
using ecms.Application.Handlers.Commands.CreateStock;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.CreateStock;

public class CreateStockCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly CreateStockCommandHandler _handler;

    public CreateStockCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new CreateStockCommandHandler(_applicationDbContext.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldCreateStock()
    {
        //Arrange
        var request = new CreateStockCommand();
        _applicationDbContext.Setup(p => p.Stocks).Returns(new Mock<DbSet<StockEntity>>().Object);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        _applicationDbContext.Verify(p => p.Stocks.AddAsync(It.IsAny<StockEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        _applicationDbContext.Verify(p => p.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
