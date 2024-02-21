﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToneVault.API.Constants;

namespace ToneVault.API.Authentication;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstant.ApiKeyHeaderName,
                    out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key Header Is Missing");
            }
            else
            {
                var apiKey = _configuration.GetValue<string>(AuthConstant.ApiKeySectionName);
                if (!apiKey.Equals(extractedApiKey))
                {
                    context.Result = new UnauthorizedObjectResult("Invalid API Key");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}