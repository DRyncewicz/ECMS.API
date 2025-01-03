using ecms.Domain.Enums;

namespace ecms.Application.Abstractions.Emails;

public interface IEmailGenerator
{
    string GenerateSupplierOrderHtmlTable(Dictionary<string, double> materialSummary, LanguageType language);

    string GenerateSupplierOrderSubject(LanguageType language);

    string GenerateOrderWelcomeMessageContent(LanguageType language, DateTimeOffset? date, int supplierOrderId);
}