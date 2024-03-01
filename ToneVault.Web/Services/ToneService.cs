using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ToneVault.Models;
using ToneVault.Web.Constants;

namespace ToneVault.Web.Services;

public class ToneService
{
    private readonly IConfiguration _configuration;

    public ToneService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<Tone>> Get()
    {
        using var client = new HttpClient();

        var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        var requestHeader = AuthConstant.ApiKeyRequestHeader;

        client.DefaultRequestHeaders.Add(requestHeader, apiKey);
        var response = await client.GetAsync("https://tonevaultapi.azurewebsites.net/tones");

        if (!response.IsSuccessStatusCode) return new List<Tone>();
        var json = await response.Content.ReadAsStringAsync();
        var tones = JsonSerializer.Deserialize<List<Tone>>(json);
        return tones;
    }

    public async Task<Tone> Get(string id)
    {
        using var client = new HttpClient();

        var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        var requestHeader = AuthConstant.ApiKeyRequestHeader;

        client.DefaultRequestHeaders.Add(requestHeader, apiKey);
        var response = await client.GetAsync($"https://tonevaultapi.azurewebsites.net/tones/{id}");

        if (!response.IsSuccessStatusCode) return new Tone();
        var json = await response.Content.ReadAsStringAsync();
        var tone = JsonSerializer.Deserialize<Tone>(json);
        return tone;
    }

    public async Task CreateTone(Tone tone)
    {
        using var client = new HttpClient();
        
        var content = JsonSerializer.Serialize(tone);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        
        var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        var requestHeader = AuthConstant.ApiKeyRequestHeader;
        client.DefaultRequestHeaders.Add(requestHeader, apiKey);
        
        var post = await client.PostAsync("https://tonevaultapi.azurewebsites.net/tones", bodyContent);
        var response = post.Content.ReadAsStringAsync();
    }
}