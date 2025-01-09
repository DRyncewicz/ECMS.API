using ecms.Application.Abstractions.Emails;
using ecms.Domain.Enums;
using SharedKernal;

namespace ecms.Application.Helpers.Emails;

public class EmailGenerator(IDateTimeProvider _dateTimeProvider) : IEmailGenerator
{
    /// <summary>
    /// Generates table with materials and quantities for supplier orders by language and dictionary<paramref name="materialSummary"/>
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public string GenerateSupplierOrderHtmlTable(Dictionary<string, double> materialSummary, LanguageType language)
    {
        var html = new System.Text.StringBuilder();

        html.AppendLine("<table border='1'>");
        html.AppendLine("<thead>");
        html.AppendLine("<tr>");
        html.AppendLine($"<th>{EmailConstants.SupplierOrders.GetMaterialNameByLanguage(language)}</th>");
        html.AppendLine($"<th>{EmailConstants.SupplierOrders.GetQuantityByLanguage(language)}</th>");
        html.AppendLine("</tr>");
        html.AppendLine("</thead>");
        html.AppendLine("<tbody>");

        foreach (var item in materialSummary)
        {
            html.AppendLine("<tr>");
            html.AppendLine($"<td>{item.Key}</td>");
            html.AppendLine($"<td>{item.Value}</td>");
            html.AppendLine("</tr>");
        }

        html.AppendLine("</tbody>");
        html.AppendLine("</table>");
        return html.ToString();
    }

    public string GenerateSupplierOrderSubject(LanguageType language)
    {
        return $"{EmailConstants.SupplierOrders.GetSubjectByLanguage(language)} {_dateTimeProvider.UtcNow.ToString("d")}";
    }

    public string GenerateOrderWelcomeMessageContent(LanguageType language, DateTimeOffset? date, int supplierOrderId)
    {
        string dateText;

        if (date is null)
        {
            return string.Format(EmailConstants.SupplierOrders.GetUnknownDateTextByLanguage(language), supplierOrderId);
        }
        else
        {
            dateText = date.Value.ToString("d");
        }
        return string.Format(EmailConstants.SupplierOrders.GetContentByLanguage(language), dateText, supplierOrderId);
    }

    public string GenerateEditedOrderWelcomeMessageContent(LanguageType language, int supplierOrderId, DateTimeOffset? date)
    {
        string dateText;

        if (date is null)
        {
            return string.Format(EmailConstants.SupplierOrders.GetEditedUnknownDateTextByLanguage(language), supplierOrderId);
        }
        else
        {
            dateText = date.Value.ToString("d");
        }
        return string.Format(EmailConstants.SupplierOrders.GetEditedContentByLanguage(language), supplierOrderId, dateText);
    }
}