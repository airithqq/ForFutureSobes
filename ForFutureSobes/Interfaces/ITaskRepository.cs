using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ForFutureSobes.Interfaces
{
    public interface ITaskRepository 
    {
        Task<TaskEntity?> GetByIdAsync(int id);
        Task<List<TaskEntity>> GetByThemeAsync(string themeName);
        Task<List<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> CreateAsync(TaskEntity task, string themeName);
        Task<bool> DeleteByThemeAsync(List<TaskEntity> tasks);
        Task SaveAsync();
        Task<List<TaskEntity>> GetAllUncompletedTasksAsync();
        Task<List<TaskEntity>> GetTasksByPriority(string priority);
    }
}
