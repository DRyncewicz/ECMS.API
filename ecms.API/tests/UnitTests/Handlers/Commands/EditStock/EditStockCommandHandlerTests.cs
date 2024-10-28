using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.EditStock;
using ecms.Domain.Entities;
using Moq;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.EditStock;

public class EditStockCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly EditStockCommandHandler _handler;

    public EditStockCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _handler = new EditStockCommandHandler(_applicationDbContext.Object, _mapper);
        List<StockEntity> stocks = new List<StockEntity>()
        {
            new StockEntity()
            {
                Id = 1,
                Name = "Dupa",
                AddressId = 1,
                Description = "Dupa",
                IsDeleted = false,
            },
            new StockEntity()
            {
                Id = 2,
                Name = "Dupa2",
                AddressId = 2,
                Description = "Dupa2",
                IsDeleted = false,
            },
            new StockEntity()
            {
                Id = 3,
                Name = "Dupa3",
                AddressId = 3,
                Description = "Dupa3",
                IsDeleted = false,
            },
        };
        _applicationDbContext.Setup(p => p.Stocks).Returns(stocks.AsQueryable().BuildMock().Object);
    }

    [Fact]
    public async Task Handle_ShouldEditStock_OnValidRequest()
    {
        //Arrange
        var request = new EditStockCommand()
        {
            StockId = 1,
            Name = "EditedDupa",
            AddressId = 1,
            Description = "EditedDupa"
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _applicationDbContext.Verify(p => p.Stocks.Update(It.IsAny<StockEntity>()), Times.Once());
    }
}
