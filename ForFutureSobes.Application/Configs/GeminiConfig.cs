
using ForFutureSobes.Application.Interfaces;
using ForFutureSobes.Model.Domain;


namespace ForFutureSobes.Application.Configs
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
