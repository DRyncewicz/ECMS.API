using ecms.Domain.Enums;

namespace ecms.Application.Helpers.Emails;

public static class EmailConstants
{
    public static class SupplierOrders
    {
        private const string MaterialNameHeaderEn = "Material Name";
        private const string MaterialNameHeaderPl = "Nazwa materiału";

        private const string QuantityHeaderEn = "Quantity";
        private const string QuantityHeaderPl = "Ilość";

        private const string OrderSubjectEn = "Order dated";
        private const string OrderSubjectPl = "Zamówienie z dnia";

        private const string WelcomeMessageContentEn = "Good afternoon.\r\nI hope this message finds you well. I am writing to submit our order for delivery on {0}. Additionally, please note our internal order number: {1}. Below are the details:\r\n";
        private const string WelcomeMessageContentPl = "Dzień dobry.\r\nPiszę, aby przesłać nasze zamówienie do dostawy w dniu {0}. Dodatkowo, proszę zanotować nasz wewnętrzny numer zamówienia: {1}. Poniżej znajdują się szczegóły:\r\n";

        private const string EditedOrderWelcomeMessageContentEn = "Good afternoon.\r\nI hope this message finds you well. Please update our order {0} for delivery on {1}. Below are the updated details:\r\n";
        private const string EditedOrderWelcomeMessageContentPl = "Dzień dobry.\r\nProszę o zaktualizowanie zamówienia: {0} do dostawy w dniu {1}. Poniżej znajdują się szczegóły:\r\n";

        private const string UnknownDeliveryDateEn = "Good afternoon.\r\nI hope this message finds you well. I am writing to submit our order for delivery. Additionally, please note our internal order number: {0}. Below are the details:\r\n";
        private const string UnknownDeliveryDatePl = "Dzień dobry.\r\nPiszę, aby przesłać nasze zamówienie do dostawy. Dodatkowo, proszę zanotować nasz wewnętrzny numer zamówienia: {0}. Poniżej znajdują się szczegóły:\r\n";

        private const string EditedOrderUnknownDeliveryDateEn = "Good afternoon.\r\nI hope this message finds you well. Please update our order {0}. Below are the updated details:\r\n";
        private const string EditedOrderUnknownDeliveryDatePl = "Dzień dobry.\r\nProszę o zaktualizowanie zamówienia: {0}. Poniżej znajdują się szczegóły:\r\n";

        public static string GetMaterialNameByLanguage(LanguageType language)
        {
            if (language == LanguageType.English)
            {
                return MaterialNameHeaderEn;
            }
            return MaterialNameHeaderPl;
        }

        public static string GetQuantityByLanguage(LanguageType language)
        {
            if (language == LanguageType.English)
            {
                return QuantityHeaderEn;
            }
            return QuantityHeaderPl;
        }

        public static string GetSubjectByLanguage(LanguageType language)
        {
            if (language == LanguageType.English)
            {
                return OrderSubjectEn;
            }
            return OrderSubjectPl;
        }

        public static string GetContentByLanguage(LanguageType language)
        {
            if (language == LanguageType.English)
            {
                return WelcomeMessageContentEn;
            }
            return WelcomeMessageContentPl;
        }

        public static string GetEditedContentByLanguage(LanguageType language)
        {
            if (language == LanguageType.English)
            {
                return EditedOrderWelcomeMessageContentEn;
            }
            return EditedOrderWelcomeMessageContentPl;
        }

        public static string GetUnknownDateTextByLanguage(LanguageType language)
        {
            if (language == LanguageType.English)
            {
                return UnknownDeliveryDateEn;
            }
            return UnknownDeliveryDatePl;
        }

        public static string GetEditedUnknownDateTextByLanguage(LanguageType language)
        {
            if (language == LanguageType.English)
            {
                return EditedOrderUnknownDeliveryDateEn;
            }
            return EditedOrderUnknownDeliveryDatePl;
        }
    }
}