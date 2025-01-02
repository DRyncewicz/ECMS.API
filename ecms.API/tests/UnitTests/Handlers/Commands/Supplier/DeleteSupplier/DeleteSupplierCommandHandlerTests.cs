using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.Supplier.DeleteSupplier;
using ecms.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.Supplier.DeleteSupplier;

public class DeleteSupplierCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly DeleteSupplierCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;

    public DeleteSupplierCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new DeleteSupplierCommandHandler(_applicationDbContext.Object, _mapper, _userService.Object, _dateTimeProvider.Object);
        _transaction = new Mock<IDbTransaction>();
        _applicationDbContext.Setup(p => p.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_transaction.Object);
        _userService.Setup(p => p.UserId).Returns("TestUserId");
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2024, 09, 22));

        var supplierContacts = new List<SupplierContactEntity>
        {
            new()
            {
                Id = 1,
                SupplierId = 1,
                IsActive = true,
                Description = "ContactDescription",
                Email = "ContactEmail",
                PhoneNumber = "ContactPhoneNumber",
                IsCommon = true,
                RepresentativeName = "ContactName"
            }
        };

        var suppliers = new List<SupplierEntity>
        {
            new()
            {
                Id = 1,
                IsActive = true,
                IsDeleted = false,
                Name = "SupplierName",
                AddressId = 1,
                SupplierContacts = supplierContacts
            },

            new()
            {
                Id = 2,
                IsActive = true,
                IsDeleted = false,
                Name = "SupplierName2",
                AddressId = 1,
            }
        };

        var dbContextResponseSupplierContacts = supplierContacts.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.SupplierContacts).Returns(dbContextResponseSupplierContacts.Object);
        var dbContextResponseSuppliers = suppliers.AsQueryable().BuildMock();
        _applicationDbContext.Setup(p => p.Suppliers).Returns(dbContextResponseSuppliers.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteSupplier()
    {
        //Arrange
        var request = new DeleteSupplierCommand(1);
        _applicationDbContext.Setup(p => p.SupplierHistories).Returns(new Mock<DbSet<SupplierHistoryEntity>>().Object);

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
        var request = new DeleteSupplierCommand(1);
        _applicationDbContext.Setup(p => p.SupplierHistories.AddAsync(It.IsAny<SupplierHistoryEntity>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception());

        //Act
        Func<Task> act = async () => await _handler.Handle(request, default);

        //Assert
        await act.Should().ThrowAsync<Exception>();
        _transaction.Verify(p => p.Rollback(), Times.Once);
    }
}