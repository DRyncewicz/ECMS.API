using AutoMapper;
using ecms.Application.Abstractions.Data;
using ecms.Application.Abstractions.Emails;
using ecms.Application.Handlers.Commands.SupplierOrder.EditSupplierOrder;
using ecms.Application.Models.Dtos.SupplierOrders;
using ecms.Domain.Entities;
using ecms.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.SupplierOrder.EditSupplierOrder
{
    public class EditSupplierOrderCommandHandlerTests : IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;
        private readonly Mock<IApplicationDbContext> _applicationDbContext;
        private readonly EditSupplierOrderCommandHandler _handler;
        private readonly Mock<IDateTimeProvider> _dateTimeProvider;
        private readonly Mock<IDbTransaction> _transaction;
        private readonly Mock<IEmailGenerator> _emailGenerator;

        public EditSupplierOrderCommandHandlerTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
            _applicationDbContext = new Mock<IApplicationDbContext>();
            _dateTimeProvider = new Mock<IDateTimeProvider>();
            _emailGenerator = new Mock<IEmailGenerator>();
            _handler = new EditSupplierOrderCommandHandler(_applicationDbContext.Object, _mapper, _dateTimeProvider.Object, _emailGenerator.Object);
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
                    Id = 1,
                    Name = "Material1",
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

            var supplierOrderMaterials = new List<SupplierOrderMaterialEntity>()
            {
                new()
                {
                    Id = 1,
                    SupplierOrderId = 1,
                    MaterialId = 1,
                    Quantity = 10,
                    Discount = 5,
                },
                new()
                {
                    Id = 2,
                    SupplierOrderId = 1,
                    MaterialId = 1,
                    Quantity = 50,
                    Discount = 5,
                }
            };

            var supplierOrders = new List<SupplierOrderEntity>()
            {
                new()
                {
                    Id = 1,
                    DeliveryDate = new DateTime(2025, 3, 22),
                    CreateDateTimeUtc = new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero),
                    Status = ecms.Domain.Enums.StatusType.Pending,
                    Supplier = new SupplierEntity()
                    {
                        Id = 1
                    },
                    SupplierContact = supplierContacts[0],
                    SupplierOrderMaterials = supplierOrderMaterials,
                },
            };

            var dbContextResponseSupplierOrders = supplierOrders.AsQueryable().BuildMockDbSet();
            _applicationDbContext.Setup(p => p.SupplierOrders).Returns(dbContextResponseSupplierOrders.Object);
            _applicationDbContext.Setup(p => p.Messages).Returns(new Mock<DbSet<MessageEntity>>().Object);
            var dbContextResponseSupplierOrderMaterials = supplierOrderMaterials.AsQueryable().BuildMockDbSet();
            _applicationDbContext.Setup(p => p.SupplierOrderMaterials).Returns(dbContextResponseSupplierOrderMaterials.Object);
            _applicationDbContext.Setup(p => p.SupplierContacts).Returns(supplierContacts.AsQueryable().BuildMock().Object);
            _applicationDbContext.Setup(p => p.Materials).Returns(materials.AsQueryable().BuildMock().Object);
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 0, 1, 0, 3, true, 1)]
        [InlineData(new int[] { 1 }, 0, 1, 1, 3, false, 0)]
        [InlineData(new int[] { 1, 0 }, 1, 1, 1, 5, true, 1)]
        [InlineData(new int[] { 0, 1, 2 }, 1, 1, 0, 3, false, 0)]
        public async Task Handle_ShouldEditSupplierOrder(int[] supplierOrderMaterialIds, int supplierOrderMaterialAddRangeCount, int supplierOrderMaterialUpdateRangeCount, int supplierOrderMaterialRemoveRangeCount, int saveChangesCount, bool sendMessage, int messageAddCount)
        {
            //Arrange
            var supplierOrderMaterialDtos = new List<EditSupplierOrderMaterialDto>();
            foreach (var supplierOrderMaterialId in supplierOrderMaterialIds)
            {
                var supplierOrderMaterialDto = new EditSupplierOrderMaterialDto()
                {
                    SupplierOrderMaterialId = supplierOrderMaterialId,
                    MaterialId = 1,
                    Quantity = 50,
                };
                supplierOrderMaterialDtos.Add(supplierOrderMaterialDto);
            }

            var command = new EditSupplierOrderCommand()
            {
                SupplierOrderId = 1,
                SupplierId = 1,
                SupplierContactId = 1,
                DeliveryDate = new DateTime(2025, 9, 22),
                EditSupplierOrderMaterialDtos = supplierOrderMaterialDtos,
                SendMessage = sendMessage,
                Language = LanguageType.English,
            };

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            _applicationDbContext.Verify(p => p.SupplierOrderMaterials.AddRange(It.IsAny<List<SupplierOrderMaterialEntity>>()), Times.Exactly(supplierOrderMaterialAddRangeCount));
            _applicationDbContext.Verify(p => p.SupplierOrderMaterials.UpdateRange(It.IsAny<List<SupplierOrderMaterialEntity>>()), Times.Exactly(supplierOrderMaterialUpdateRangeCount));
            _applicationDbContext.Verify(p => p.SupplierOrderMaterials.RemoveRange(It.IsAny<List<SupplierOrderMaterialEntity>>()), Times.Exactly(supplierOrderMaterialRemoveRangeCount));
            _applicationDbContext.Verify(p => p.Messages.AddAsync(It.IsAny<MessageEntity>(), It.IsAny<CancellationToken>()), Times.Exactly(messageAddCount));
            _applicationDbContext.Verify(p => p.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(saveChangesCount));
            _transaction.Verify(p => p.Commit(), Times.Once());
        }

        [Fact]
        public async Task Handle_ShouldRollbackTransaction_WhenExceptionOccurs()
        {
            //Arrange
            var supplierOrderMaterialDtos = new List<EditSupplierOrderMaterialDto>()
            {
                new()
                {
                    SupplierOrderMaterialId = 1,
                    MaterialId = 1,
                    Quantity = 50,
                }
            };
            var command = new EditSupplierOrderCommand()
            {
                SupplierOrderId = 1,
                SupplierId = 1,
                SupplierContactId = 1,
                DeliveryDate = new DateTime(2025, 9, 22),
                EditSupplierOrderMaterialDtos = supplierOrderMaterialDtos,
                SendMessage = false,
                Language = LanguageType.English,
            };
            _applicationDbContext.Setup(p => p.SupplierOrderMaterials.UpdateRange(It.IsAny<List<SupplierOrderMaterialEntity>>()))
                         .Throws(new Exception("Simulated exception"));

            //Act
            Func<Task> act = async () => await _handler.Handle(command, default);

            //Assert
            await Assert.ThrowsAsync<Exception>(act);
            _transaction.Verify(p => p.Rollback(), Times.Once);
        }
    }
}