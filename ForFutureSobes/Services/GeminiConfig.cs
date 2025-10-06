using System.Runtime;
using ForFutureSobes.Domain;
using ForFutureSobes.Interfaces;

namespace ForFutureSobes.Services
{
    public class GeminiConfig:IGeminiConfig
    {
        private readonly GeminiSettings _settings;
        public GeminiConfig(GeminiSettings settings)
        {
                _settings = settings;
        }

        public string GetUrl()
        {
            var url = $"{_settings.BaseUrl}/{_settings.Version}/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";
            return url ;
        }
    }
}
