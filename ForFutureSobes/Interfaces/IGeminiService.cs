using ForFutureSobes.Domain;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using ForFutureSobes.DTOs;

namespace ForFutureSobes.Interfaces
{
    public interface IGeminiService
    {
        public Task<string> SendAsync(string prompt);
        Task <string> GetTaskSummariesAsync(int taskId);
    }
}