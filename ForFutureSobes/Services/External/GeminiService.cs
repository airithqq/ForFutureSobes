using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ForFutureSobes.Domain.Gemini;
using ForFutureSobes.Interfaces;
using Microsoft.Extensions.Options;
using ForFutureSobes.DTOs;



namespace ForFutureSobes.Services.External
{
    //change name of this class
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiSettings _settings;

        public GeminiService(HttpClient httpClient, IOptions<GeminiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<string> GenerateTextAsync(string prompt, CancellationToken cancellationToken = default)
        {
            var request = new
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

            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-pro:generateContent?key=AIzaSyAD_rpycjNMUYEtLISixTZYRGTXo-yslRs";
            var response = await _httpClient.PostAsJsonAsync(url, request, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException($"Gemini API error {response.StatusCode}: {content}");

            using var doc = JsonDocument.Parse(content);
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? "(no response)";
        }
    }
}
