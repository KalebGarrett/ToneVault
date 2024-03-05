using System.Text.Json.Serialization;

namespace ToneVault.Models;

public class AddToneResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("traceId")]
    public string TraceId { get; set; }

    [JsonPropertyName("errors")]
    public Errors Errors { get; set; }
}

public class Errors
{
    [JsonPropertyName("Id")]
    public List<string> Id { get; set; }
}
