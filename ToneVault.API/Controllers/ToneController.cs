using Microsoft.AspNetCore.Mvc;
using ToneVault.Models;

namespace ToneVault.API.Controllers;

[ApiController]
[Route("tones")]
public class ToneController : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
        var tones = new List<Tone>();
        tones.Add(new Tone());
        return Ok(tones);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(new Tone());
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(Tone tone)
    {
        return Created("", tone);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Tone tone)
    {
        return Ok(tone);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return NoContent();
    }
}