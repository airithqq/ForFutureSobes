using ForFutureSobes.Domain;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using ForFutureSobes.Interfaces;

namespace ForFutureSobes.Services
{
    public class GeminiService:IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeminiService> _logger;
        private readonly IGeminiConfig _config;

        public GeminiService(HttpClient httpClient,IGeminiConfig config, ILogger<GeminiService> logger)
        {
            _httpClient = httpClient;
            _config = config;   
            _logger = logger;
        }

        public async Task<string> SendAsync(string prompt)
        {
            
            _logger.LogInformation("Gemini request URL: {Url}", _config.GetUrl());

            var payload = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(payload);
            var request = new HttpRequestMessage(HttpMethod.Post, _config.GetUrl())
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseJson);
                var text = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                return text ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gemini request failed");
                throw;
            }
        }
    }
}

