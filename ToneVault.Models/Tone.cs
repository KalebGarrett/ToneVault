namespace ToneVault.Models;

public class Tone : BaseResource
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string CreatedBy { get; set; }
    public Equalizer Equalizer { get; set; }
    public int Reverb { get; set; }
    public int Volume { get; set; }
    public string Description { get; set; }
}

public class Equalizer
{
    public int Gain { get; set; }
    public int Bass { get; set; }
    public int Middle { get; set; }
    public int Treble { get; set; }
}