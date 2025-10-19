using ForFutureSobes.Model.Domain;

namespace ForFutureSobes.Infrastructure.Interfaces
{
    public interface IGeminiRepository
    {
        Task<TaskEntity?> GetPromptFromTask(int taskId);
    }
}
