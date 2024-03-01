using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ToneVault.API.Authentication;
using ToneVault.API.Repositories.Interfaces;
using ToneVault.Models;

namespace ToneVault.API.Controllers;

[ApiController]
[Route("tones")]
public class ToneController : ControllerBase
{
    private readonly IMongoRepository<Tone> _toneRepository;

    public ToneController(IMongoRepository<Tone> toneRepository)
    {
        _toneRepository = toneRepository;
    }

    [HttpGet("")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<IActionResult> Get([FromHeader(Name = "x-api-key")] [Required] string header)
    {
        var tones = await _toneRepository.GetAll();

        if (!tones.Any())
        {
            return NotFound();
        }

        return Ok(tones);
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<IActionResult> GetById(string id, [FromHeader(Name = "x-api-key")] [Required] string header)
    {
        var tone = await _toneRepository.GetById(id);

        if (tone == null)
        {
            return NotFound();
        }

        return Ok(tone);
    }

    [HttpPost("")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<IActionResult> Create(Tone tone, [FromHeader(Name = "x-api-key")] [Required] string header)
    {
        tone = await _toneRepository.Create(tone);
        return Created(tone.Id, tone);
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<IActionResult> Update(string id, Tone tone,
        [FromHeader(Name = "x-api-key")] [Required]
        string header)
    {
        tone = await _toneRepository.Update(id, tone);

        if (id != tone.Id)
        {
            return NotFound();
        }

        return Ok(tone);
    }

    [HttpDelete("{id}")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<IActionResult> Delete(string id, [FromHeader(Name = "x-api-key")] [Required] string header)
    {
        await _toneRepository.Delete(id);
        return NoContent();
    }
}