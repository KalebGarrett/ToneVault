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

        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        var tone = JsonSerializer.Deserialize<Tone>(json);
        return tone;
    }

    public async Task<ToneResponse> Create(Tone tone)
    {
        using var client = new HttpClient();

        var content = JsonSerializer.Serialize(tone);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        var requestHeader = AuthConstant.ApiKeyRequestHeader;
        client.DefaultRequestHeaders.Add(requestHeader, apiKey);

        var createToneResponse = await client.PostAsync("https://tonevaultapi.azurewebsites.net/tones", bodyContent);
        var createToneContent = await createToneResponse.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<ToneResponse>(createToneContent, new JsonSerializerOptions());
        if (!createToneResponse.IsSuccessStatusCode) return response;
        return new ToneResponse();
    }

    public async Task<ToneResponse> Update(string id, Tone tone)
    {
        using var client = new HttpClient();

        var content = JsonSerializer.Serialize(tone);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        var requestHeader = AuthConstant.ApiKeyRequestHeader;
        client.DefaultRequestHeaders.Add(requestHeader, apiKey);

        var editToneResponse = await client.PutAsync($"https://tonevaultapi.azurewebsites.net/tones/{id}", bodyContent);
        var editToneContent = await editToneResponse.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<ToneResponse>(editToneContent, new JsonSerializerOptions());
        if (!editToneResponse.IsSuccessStatusCode) return response;
        return new ToneResponse();
    }

    public async Task Delete(string id)
    {
        using var client = new HttpClient();

        var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
        var requestHeader = AuthConstant.ApiKeyRequestHeader;
        client.DefaultRequestHeaders.Add(requestHeader, apiKey);

        await client.DeleteAsync($"https://tonevaultapi.azurewebsites.net/tones/{id}");
    }
}