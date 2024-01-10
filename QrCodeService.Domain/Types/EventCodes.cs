namespace QrCodeService.Domain.Types;

public struct EventCodes
{
    public const string NewResponse = "route.new.response";
    public const string DocFlow = "docflow";
    public const string ServiceDocResponse = "service.doc.response";
    public const string EventDocSend = "doc.send";
    public const string EventConsumerType = "rrr";
    public const string ServiceDocRequest = "service.doc.request";
    public const string ClientError = "client.error";
    public const string ServerError = "server.error";
    public const string QrCodeFromOperator = "qr_from_rrr_operator";
    public const string QrCodeRequest = "qr.code.request";
}