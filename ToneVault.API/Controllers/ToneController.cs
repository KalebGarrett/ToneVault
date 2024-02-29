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
    public async Task<IActionResult> Get([FromHeader(Name = "x-api-key")][Required] string header)
    {
        var tones = await _toneRepository.GetAll();

        if (!tones.Any())
        {
            return NoContent();
        }

        return Ok(tones);
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<IActionResult> GetById(string id, [FromHeader(Name = "x-api-key")][Required] string header)
    {
        var tone = await _toneRepository.GetById(id);

        if (tone == null)
        {
            return NoContent();
        }

        return Ok(tone);
    }

    [HttpPost("")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public async Task<IActionResult> Create(Tone tone, [FromHeader(Name = "x-api-key")][Required] string header)
    {
        tone = await _toneRepository.Create(tone);
        return Created(tone.Id, tone);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Tone tone)
    {
        if (id != tone.Id)
        {
            return BadRequest();
        }

        tone = await _toneRepository.Update(id, tone);

        if (tone == null)
        {
            return NotFound();
        }

        return Ok(tone);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _toneRepository.Delete(id);
        return NoContent();
    }
}