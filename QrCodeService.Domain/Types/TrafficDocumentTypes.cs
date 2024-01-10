namespace QrCodeService.Domain.Types
{
    public struct TrafficDocumentTypes
    {
        public static Dictionary<string, string> ArticleType { get; } =
            new Dictionary<string, string>() {
                {"G1J","article_1"},
                {"G2J","article_2"},
                {"G3J","article_3"},
                {"G4J","article_4"},
                {"G7J","article_7"}
        };
        public const string DocFlowType = "GJ";

        public const string EventType = "answer_from_operator";

        public const string InformRefine = "KLK";

        public const string InformReceive = "LLR";

        public const string TransportType = "TransportInvoice";

    }
}