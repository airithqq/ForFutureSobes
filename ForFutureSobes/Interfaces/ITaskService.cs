using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ForFutureSobes.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForFutureSobes.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskEntity>> GetTasksByThemeAsync(string themeName);
        Task<TaskEntity> GetTaskByIdAsync(int id, string themeName);
        Task<TaskEntity> CreateTaskAsync(TaskEntity task, string themeName);
        Task<bool> UpdateTaskAsync(int id, TaskEntity updatedTask, string themeName);
        Task<bool> DeleteTaskAsync(int id, string themeName);
        Task<List<TaskEntity>> GetAllTasksAsync();
      
    }
}
