using System.Text;
using System.Text.Json;
using AutoMapper;
using ForFutureSobes.DTOs;
using ForFutureSobes.Helper;
using ForFutureSobes.Interfaces;
using ForFutureSobes.Repository;

namespace ForFutureSobes.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeminiService> _logger;
        private readonly IGeminiConfig _config;
        private readonly IMapper _mapper;
        private readonly IGeminiRepository _geminiRepository;

        public GeminiService(HttpClient httpClient, IGeminiConfig config, ILogger<GeminiService> logger, IMapper mapper, IGeminiRepository geminiRepository)
        {
            _httpClient = httpClient;
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _geminiRepository = geminiRepository;
        }

        public async Task<string> GetTaskSummariesAsync(int taskId)
        {
            var task = await _geminiRepository.GetPromptFromTask(taskId);

            if (task is null)
                throw new KeyNotFoundException($"Task with ID {taskId} not found.");

            var dto = _mapper.Map<TaskSummaryDTO>(task);
            return PromptBuilder.BuildGeminiPrompt(dto);

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

