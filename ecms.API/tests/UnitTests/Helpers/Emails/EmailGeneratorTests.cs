using ecms.Application.Helpers.Emails;
using ecms.Domain.Enums;
using FluentAssertions;
using Moq;
using SharedKernal;

namespace UnitTests.Helpers.Emails;

public class EmailGeneratorTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProvider;
    private readonly EmailGenerator _emailGenerator;

    public EmailGeneratorTests()
    {
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _emailGenerator = new EmailGenerator(_dateTimeProvider.Object);
    }

    [Fact]
    public void GenerateSupplierOrderHtmlTable_Should_ReturnCorrectHtml()
    {
        //Arrange
        var materialSummary = new Dictionary<string, double>
        {
            { "A", 10 }
        };
        var language = LanguageType.English;

        //Act
        var result = _emailGenerator.GenerateSupplierOrderHtmlTable(materialSummary, language);

        //Assert
        result.Should().Be("<table border='1'>\r\n<thead>\r\n<tr>\r\n<th>Material Name</th>\r\n<th>Quantity</th>\r\n</tr>\r\n</thead>\r\n<tbody>\r\n<tr>\r\n<td>A</td>\r\n<td>10</td>\r\n</tr>\r\n</tbody>\r\n</table>\r\n");
    }

    [Fact]
    public void GenerateSupplierOrderSubject_Should_ReturnCorrectSubject()
    {
        //Arrange
        _dateTimeProvider.Setup(p => p.UtcNow).Returns(new DateTime(2025, 9, 22));
        var language = LanguageType.English;

        //Act
        var result = _emailGenerator.GenerateSupplierOrderSubject(language);

        //Assert
        result.Should().Be($"Order dated {_dateTimeProvider.Object.UtcNow.ToString("d")}");
    }

    [Fact]
    public void GenerateOrderWelcomeMessageContent_Should_ReturnCorrectMessage()
    {
        //Arrange
        var language = LanguageType.Polish;
        var date = new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero);
        var dateText = date.ToString("d");
        var supplierOrderId = 1;

        //Act
        var result = _emailGenerator.GenerateOrderWelcomeMessageContent(language, date, supplierOrderId);

        //Assert
        result.Should().Be($"Dzień dobry.\r\nPiszę, aby przesłać nasze zamówienie do dostawy w dniu {dateText}. Dodatkowo, proszę zanotować nasz wewnętrzny numer zamówienia: 1. Poniżej znajdują się szczegóły:\r\n");
    }

    [Fact]
    public void GenerateOrderWelcomeMessageContent_Should_ReturnCorrectMessage_IfDateIsNull()
    {
        //Arrange
        var language = LanguageType.Polish;
        var supplierOrderId = 1;

        //Act
        var result = _emailGenerator.GenerateOrderWelcomeMessageContent(language, null, supplierOrderId);

        //Assert
        result.Should().Be("Dzień dobry.\r\nPiszę, aby przesłać nasze zamówienie do dostawy. Dodatkowo, proszę zanotować nasz wewnętrzny numer zamówienia: 1. Poniżej znajdują się szczegóły:\r\n");
    }

    [Fact]
    public void GenerateEditedOrderWelcomeMessageContent_Should_ReturnCorrectMessage()
    {
        //Arrange
        var language = LanguageType.English;
        var date = new DateTimeOffset(2025, 9, 22, 12, 0, 0, TimeSpan.Zero);
        var dateText = date.ToString("d");
        var supplierOrderId = 1;

        //Act
        var result = _emailGenerator.GenerateEditedOrderWelcomeMessageContent(language, supplierOrderId, date);

        //Assert
        result.Should().Be($"Good afternoon.\r\nI hope this message finds you well. Please update our order 1 for delivery on {dateText}. Below are the updated details:\r\n");
    }

    [Fact]
    public void GenerateEditedOrderWelcomeMessageContent_Should_ReturnCorrectMessage_IfDateIsNull()
    {
        //Arrange
        var language = LanguageType.English;
        var supplierOrderId = 1;

        //Act
        var result = _emailGenerator.GenerateEditedOrderWelcomeMessageContent(language, supplierOrderId, null);

        //Assert
        result.Should().Be($"Good afternoon.\r\nI hope this message finds you well. Please update our order 1. Below are the updated details:\r\n");
    }
}