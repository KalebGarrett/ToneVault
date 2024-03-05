using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToneVault.Models;

public class Tone : BaseResource
{
    [Required(ErrorMessage = "Tone name is required")]
    [MaxLength(35)]
    [JsonPropertyName("toneName")]
    public string ToneName { get; set; }

    [Required(ErrorMessage = "Genre is required")]
    [MaxLength(35)]
    [JsonPropertyName("genre")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "Your name is required")]
    [MaxLength(35)]
    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; }

    [JsonPropertyName("equalizer")] public Equalizer Equalizer { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(100)]
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class Equalizer
{
    [JsonPropertyName("gain")] public int Gain { get; set; }
    [JsonPropertyName("bass")] public int Bass { get; set; }
    [JsonPropertyName("middle")] public int Middle { get; set; }
    [JsonPropertyName("treble")] public int Treble { get; set; }
    [JsonPropertyName("reverb")] public int Reverb { get; set; }
    [JsonPropertyName("volume")] public int Volume { get; set; }
}