using AutoMapper;
using ecms.Application.Abstractions.Auth;
using ecms.Application.Abstractions.Data;
using ecms.Application.Handlers.Commands.EditSupplier;
using ecms.Application.Models.Dtos.Suppliers;
using ecms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernal;
using System.Data;
using UnitTests.Mapping;

namespace UnitTests.Handlers.Commands.EditSupplier;

public class EditSupplierCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly IMapper _mapper;
    private readonly Mock<IApplicationDbContext> _applicationDbContext;
    private readonly EditSupplierCommandHandler _handler;
    private readonly Mock<ICurrentUserService> _userService;
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly Mock<IDbTransaction> _transaction;

    public EditSupplierCommandHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _applicationDbContext = new Mock<IApplicationDbContext>();
        _userService = new Mock<ICurrentUserService>();
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _handler = new EditSupplierCommandHandler(_applicationDbContext.Object, _mapper, _dateTimeProvider.Object, _userService.Object);
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
            },
            new()
            {
                Id = 2,
                SupplierId = 1,
                IsActive = true,
                Description = "ContactDescription2",
                Email = "ContactEmail2",
                PhoneNumber = "ContactPhoneNumber2",
                IsCommon = true,
                RepresentativeName = "ContactName2"
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
        _applicationDbContext.Setup(p => p.SupplierHistories).Returns(new Mock<DbSet<SupplierHistoryEntity>>().Object);
    }

    [Theory]
    [InlineData(new int[] { 1, 2 }, 0, 1, 0, 3)]
    [InlineData(new int[] { 1 }, 0, 1, 1, 4)]
    [InlineData(new int[] { 1, 0 }, 1, 1, 1, 5)]
    [InlineData(new int[] { 0, 1, 2 }, 1, 1, 0, 4)]
    public async Task Handle_ShouldEditSupplier(int[] contactIds, int supplierContactAddRangeCount, int supplierContactUpdateRangeCount, int supplierContactRemoveRangeCount, int saveChangesCount)
    {
        //Arrange
        var contactsDto = new List<CreateSupplierContactDto>();
        foreach (var contactId in contactIds)
        {
            var supplierContactDto = new CreateSupplierContactDto()
            {
                SupplierId = 1,
                Id = contactId,
            };
            contactsDto.Add(supplierContactDto);
        }

        var request = new EditSupplierCommand()
        {
            Name = "Test",
            AddressId = 1,
            SupplierId = 1,
            Contacts = contactsDto
        };

        //Act
        var result = await _handler.Handle(request, default);

        //Assert
        _applicationDbContext.Verify(p => p.SupplierHistories.AddAsync(It.IsAny<SupplierHistoryEntity>(), It.IsAny<CancellationToken>()), Times.Once());
        _applicationDbContext.Verify(p => p.SupplierContacts.AddRange(It.IsAny<List<SupplierContactEntity>>()), Times.Exactly(supplierContactAddRangeCount));
        _applicationDbContext.Verify(p => p.SupplierContacts.UpdateRange(It.IsAny<List<SupplierContactEntity>>()), Times.Exactly(supplierContactUpdateRangeCount));
        _applicationDbContext.Verify(p => p.SupplierContacts.RemoveRange(It.IsAny<List<SupplierContactEntity>>()), Times.Exactly(supplierContactRemoveRangeCount));
        _applicationDbContext.Verify(p => p.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(saveChangesCount));
        _transaction.Verify(p => p.Commit(), Times.Once());
    }
}