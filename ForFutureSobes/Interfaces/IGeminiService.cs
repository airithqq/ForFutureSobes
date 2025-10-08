using ForFutureSobes.Domain;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
 

namespace ForFutureSobes.Interfaces
{
    public interface IGeminiService
    {
        public Task<string> SendAsync(string prompt);
    }
}
