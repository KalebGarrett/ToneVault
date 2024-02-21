﻿using System.Text.Json;
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
        var requestHeader = _configuration.GetValue<string>(AuthConstant.ApiKeyRequestHeader);

        client.DefaultRequestHeaders.Add(requestHeader, apiKey);
        var response = await client.GetAsync("https://tonevaultapi.azurewebsites.net/tones");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var tones = JsonSerializer.Deserialize<List<Tone>>(json);
            Console.WriteLine(tones); // Remove test case
            return tones;
        }

        return new List<Tone>();
    }
}