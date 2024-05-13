﻿using Newtonsoft.Json;

using Silicon.Blazor.Data;
using Silicon.Blazor.Factories;
using Silicon.Blazor.Models;
using System.Net;
using System.Text;

namespace Silicon.Blazor.Services;

public class UserService(HttpClient httpClient, IConfiguration configuration)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    public async Task<ResponseResult> ManageSubscription(bool isSubscribed, string email)
    {
        try
        {
            var apiUrl = _configuration.GetValue<string>("ConnectionStrings:ToggleSubscription");
            var requestData = new { Email = email, IsSubscribed = isSubscribed };
            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "appliction/json");
            var httpResponse = await _httpClient.PostAsync(apiUrl, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                return ResponseFactory.Ok();
            }
            else if (httpResponse.StatusCode == HttpStatusCode.Conflict)
            {
                return ResponseFactory.Exists();
            }
            else
                return ResponseFactory.Error();

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error($"ERROR: {ex.Message}");
        }
    }
}
