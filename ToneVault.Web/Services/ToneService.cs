using System.Net;
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

        if (!response.IsSuccessStatusCode) return null;
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

        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        var tone = JsonSerializer.Deserialize<Tone>(json);
        return tone;
    }

    public async Task<AddToneResponse> AddTone(Tone tone)
    {
        using var client = new HttpClient();
        
        var content = JsonSerializer.Serialize(tone);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        
        var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        var requestHeader = AuthConstant.ApiKeyRequestHeader;
        client.DefaultRequestHeaders.Add(requestHeader, apiKey);
        
        var createToneResult = await client.PostAsync("https://tonevaultapi.azurewebsites.net/tones", bodyContent);
        var createToneContent = await createToneResult.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<AddToneResponse>(createToneContent, new JsonSerializerOptions());
        if (!createToneResult.IsSuccessStatusCode) return response;
        return new AddToneResponse();
    }

    public async Task<Tone> EditTone(string id, Tone tone)
    {
        using var client = new HttpClient();
        
        var content = JsonSerializer.Serialize(tone);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        
        var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        var requestHeader = AuthConstant.ApiKeyRequestHeader;
        client.DefaultRequestHeaders.Add(requestHeader, apiKey);

        var editToneResult = await client.PutAsync($"https://tonevaultapi.azurewebsites.net/tones/{id}", bodyContent);
        var editToneContent = await editToneResult.Content.ReadAsStringAsync();
        tone = JsonSerializer.Deserialize<Tone>(editToneContent, new JsonSerializerOptions());
        if (!editToneResult.IsSuccessStatusCode) return null;
        return tone;
    }
}