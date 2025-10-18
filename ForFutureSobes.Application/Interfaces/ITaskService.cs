using ForFutureSobes.Application.DTOs;
using ForFutureSobes.Model.Domain;

namespace ForFutureSobes.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskEntity>> GetTasksByThemeAsync(string themeName);
        Task<TaskEntity> GetTaskByIdAsync(int id);
        Task<TaskEntity> CreateTaskAsync(TaskEntity task, string themeName);
        Task<TaskEntity> UpdateTaskAsync( int id, TaskEntity updatedTask, string updatedTheme);
        Task<bool> DeleteTaskAsync(string themeName);
        Task<List<ResponseDTO>> GetAllTasksAsync();
        Task<List<ResponseDTO>> GetAllUncompletedTasksAsync();

        Task<List<ResponseDTO>> GetTasksByPrority(string priority);
    }
}
