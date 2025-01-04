using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Abstractions.Emails;
using ecms.Application.Handlers.Commands.SupplierOrder.CreateSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Entities;
using ecms.Domain.Enums;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.SupplierOrder.CreateSupplierOrder;

public class CreateSupplierOrderCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly CreateSupplierOrderCommandHandler _handler;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;
    private readonly Mock<IEmailGenerator> _emailGenerator;

    public CreateSupplierOrderCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _emailGenerator = new Mock<IEmailGenerator>();
        _handler = new CreateSupplierOrderCommandHandler(_applicationDbContext.Object, _mapper, _dateTimeProvider.Object, _emailGenerator.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2025, 09, 22));
        _emailGenerator.Setup(p => p.GenerateSupplierOrderSubject(It.IsAny<LanguageType>())).Returns("Subject");
        _emailGenerator.Setup(p => p.GenerateOrderWelcomeMessageContent(It.IsAny<LanguageType>(), It.IsAny<DateTimeOffset>(), It.IsAny<int>())).Returns("WelcomeMessage");
        _emailGenerator.Setup(p => p.GenerateSupplierOrderHtmlTable(It.IsAny<Dictionary<string, double>>(), It.IsAny<LanguageType>())).Returns("Table");
        var message = new MessageEntity()
        {
            Id = 1,
        };

        var materials = new List<MaterialEntity>()
        {
            new()
            {
                Name = "Kox",
                Id = 1,
            }
        };

        var supplierContacts = new List<SupplierContactEntity>()
        {
            new()
            {
                Id = 1,
                Email = "pbojarski18@gmail.com"
            }
        };
        _applicationDbContext.Setup(p => p.Messages).Returns(new Mock<DbSet<MessageEntity>>().Object);
        _applicationDbContext.Setup(p => p.SupplierContacts).Returns(supplierContacts.AsQueryable().BuildMock().Object);
        _applicationDbContext.Setup(p => p.Materials).Returns(materials.AsQueryable().BuildMock().Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateSupplierOrder()
    {
        //Arrange
        var dictionary = new Dictionary<string, int>()
        {
            { "Kox", 1 },
        };

        var supplierOrderMaterialDtos = new List<CreateSupplierOrderMaterialDto>()
        {
            new()
            {
                MaterialId = 1,
                Quantity = 1,
                PricePerUnit = new ecms.Domain.ValueObjects.Price(25, ecms.Domain.ValueObjects.Currency.Usd),
                Discount = 1,
            }
        };

        var request = new CreateSupplierOrderCommand()
        {
            SupplierId = 1,
            SendMessage = true,
            SupplierContactId = 1,
            SupplierOrderMaterialDtos = supplierOrderMaterialDtos,
            Language = LanguageType.English,
            DeliveryDate = new DateTime(2025, 9, 22, 12, 0, 0)
        };
        _applicationDbContext.Setup(p => p.SupplierOrders).Returns(new Mock<DbSet<SupplierOrderEntity>>().Object);
        _applicationDbContext.Setup(p => p.SupplierOrderMaterials).Returns(new Mock<DbSet<SupplierOrderMaterialEntity>>().Object);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateSupplierOrder_WithoutSendingMessage()
    {
        //Arrange
        var supplierOrderMaterialDtos = new List<CreateSupplierOrderMaterialDto>()
        {
            new()
            {
                MaterialId = 1,
                Quantity = 1,
                PricePerUnit = new ecms.Domain.ValueObjects.Price(25, ecms.Domain.ValueObjects.Currency.Usd),
                Discount = 1,
            }
        };

        var request = new CreateSupplierOrderCommand()
        {
            SupplierId = 1,
            SendMessage = false,
            SupplierContactId = 1,
            SupplierOrderMaterialDtos = supplierOrderMaterialDtos,
            Language = LanguageType.English,
            DeliveryDate = new DateTime(2025, 9, 22, 12, 0, 0)
        };
        _applicationDbContext.Setup(p => p.SupplierOrders).Returns(new Mock<DbSet<SupplierOrderEntity>>().Object);
        _applicationDbContext.Setup(p => p.SupplierOrderMaterials).Returns(new Mock<DbSet<SupplierOrderMaterialEntity>>().Object);

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _transaction.Verify(p => p.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollbackTransaction_WhenExceptionOccurs()
    {
        //Arrange
        var supplierOrderMaterialDtos = new List<CreateSupplierOrderMaterialDto>()
        {
            new()
            {
                MaterialId = 1,
                Quantity = 1,
                PricePerUnit = new ecms.Domain.ValueObjects.Price(25, ecms.Domain.ValueObjects.Currency.Usd),
                Discount = 1,
            }
        };

        var request = new CreateSupplierOrderCommand()
        {
            SupplierId = 1,
            SendMessage = true,
            SupplierContactId = 1,
            SupplierOrderMaterialDtos = supplierOrderMaterialDtos,
            Language = LanguageType.English,
            DeliveryDate = new DateTime(2025, 9, 22, 12, 0, 0)
        };
        _applicationDbContext.Setup(p => p.SupplierOrders.AddAsync(It.IsAny<SupplierOrderEntity>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception("Simulated exception"));
        _applicationDbContext.Setup(p => p.SupplierOrderMaterials).Returns(new Mock<DbSet<SupplierOrderMaterialEntity>>().Object);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        // Assert
        await act.Should().ThrowAsync<Exception>();
        _transaction.Verify(p => p.Rollback(), Times.Once);
    }
}