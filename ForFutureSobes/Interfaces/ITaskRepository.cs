using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ForFutureSobes.Interfaces
{
    public interface ITaskRepository 
    {
        Task<TaskEntity?> GetById(int id);
        Task<List<TaskEntity>> GetByTheme(string themeName);
        Task<List<TaskEntity>> GetAll();
        Task<TaskEntity?> Create(TaskEntity task, string themeName);
        Task<bool> DeleteByTheme(List<TaskEntity> tasks);
        Task Save();
    }
}
