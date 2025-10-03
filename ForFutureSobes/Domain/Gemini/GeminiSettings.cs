namespace ForFutureSobes.Domain.Gemini
{
    public class GeminiSettings
    {
        public const string SectionName = "GeminiSettings";

        public string ApiKey { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
    }
}
