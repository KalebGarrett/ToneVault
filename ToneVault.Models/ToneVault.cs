namespace ToneVault.Models;

public class ToneVault : BaseResource
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public int Bass { get; set; }
    public int Middle { get; set; }
    public int Treble { get; set; }
    public int Gain { get; set; }
    public int Reverb { get; set; }
    public int Volume { get; set; }
    public string Description { get; set; }
}