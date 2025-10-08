using ForFutureSobes.Domain;
namespace ForFutureSobes.Interfaces
{
    public interface IGeminiRepository
    {
        Task<TaskEntity?> GetPromptFromTask(int taskId);
    }
}
