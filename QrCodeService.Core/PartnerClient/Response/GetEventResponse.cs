using Newtonsoft.Json;

namespace QrCodeService.Core.PartnerClient.Response;

public class GetEventResponse
{
    /// <summary>
    /// The status of the request handle.
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Description of the query result.
    /// </summary>
    [JsonProperty("article")]
    public string? Article { get; set; }

    [JsonProperty("events")]
    public List<GetEventResponseEvents>? Events { get; set; }
}

public sealed class GetEventResponseEvents
{
    /// <summary>
    /// [a - f0 - 9]- event ID
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Code of the published event.
    /// </summary>
    [JsonProperty("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Event Id ( = OperatorMessageId)
    /// </summary>
    [JsonProperty("parent")]
    public string? Parent { get; set; }

    /// <summary>
    /// ID of the event sender.
    /// </summary>
    [JsonProperty("producer")]
    public string? Producer { get; set; }

    /// <summary>
    /// ID of the event receiver.
    /// </summary>
    [JsonProperty("consumer")]
    public string[]? Consumer { get; set; }

    /// <summary>
    /// Date of create event
    /// </summary>
    [JsonProperty("created")]
    public string? Created { get; set; }

    /// <summary>
    /// Parameters for handle event
    /// </summary>
    [JsonProperty("data")]
    public object? Data { get; set; }
}