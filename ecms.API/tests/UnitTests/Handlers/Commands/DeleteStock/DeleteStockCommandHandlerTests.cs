using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.DeleteStock;
using ecms.Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTests.Handlers.Commands.DeleteStock;

public class DeleteStockCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly DeleteStockCommandHandler _handler;

    public DeleteStockCommandHandlerTests()
    {
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new DeleteStockCommandHandler(_applicationDbContext.Object);
        
        var stocks = new List<StockEntity>
        {
            new()
            {
                Id = 1,
                Name = "Stock",
                Address = new AddressEntity
                {
                    Id = 1
                },
                StockLevels = new List<StockLevelEntity>()
                {
                    new StockLevelEntity()
                    {
                        Quantity = 10,
                        Material = new MaterialEntity()
                        {
                            Id = 1
                        }
                    }
                }               
            },

            new()
            {
                Id = 2,
                Name = "Stock2",
                Address = new AddressEntity
                {
                    Id = 1
                },
                StockLevels = new List<StockLevelEntity>()
                {
                    
                }
            },
        };
        _applicationDbContext.Setup(p => p.Stocks).Returns(stocks.AsQueryable().BuildMock().Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnErrorString_IfThereAreStockLevels()
    {
        //Arrange
        var request = new DeleteStockCommand(1);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().Be("Cannot delete stock because there are products in. To delete stock, create internal transfer first");
    }

    [Fact]
    public async Task Handle_ShouldDeleteStock_IfThereAreNoStockLevels()
    {
        //Arrange
        var request = new DeleteStockCommand(2);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        result.Value.Should().Be("");
    }

    [Fact]
    public async Task Handle_ShouldReturnException_IfStockIsNotFound()
    {
        //Arrange
        var request = new DeleteStockCommand(3);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}