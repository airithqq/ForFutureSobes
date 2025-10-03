using ForFutureSobes.Domain.Gemini;
using static ForFutureSobes.Domain.Gemini.GeminiSettings;

namespace ForFutureSobes.Interfaces
{
    public interface IGeminiService
    {
        Task<string> GenerateTextAsync(string prompt, CancellationToken cancellationToken = default);
    }
}
