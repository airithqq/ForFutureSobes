namespace ForFutureSobes.Application.Interfaces
{
    public interface IGeminiService
    {
        public Task<string> SendAsync(string prompt);
        Task <string> GetTaskSummariesAsync(string variant,int taskId);
    }
}