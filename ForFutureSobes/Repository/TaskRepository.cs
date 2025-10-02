using ForFutureSobes.Interfaces;
using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ForFutureSobes.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ForFutureSobesDbContext context;
        public TaskRepository(ForFutureSobesDbContext context)
        {
            this.context = context;
        }

        public async Task<TaskEntity?> CreateAsync(TaskEntity task, string themeName)
        {
            var theme = await context.Themes.FirstOrDefaultAsync(x => x.Name == themeName);

            task.ThemeId = theme.Id;
            context.TaskEntities.Add(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteByThemeAsync(List<TaskEntity> tasks)
        {
            
            context.TaskEntities.RemoveRange(tasks);
            await context.SaveChangesAsync();  
            return true;
        }

        public async Task<List<TaskEntity>> GetAllAsync()
        {
            return await context.TaskEntities
                .Include(x=>x.Theme)
                .ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            return await context.TaskEntities
                .Include(x => x.Theme)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<TaskEntity>> GetByThemeAsync(string themeName)
        {
            return await context.TaskEntities
                .Include(x => x.Theme)
                .Where(x => x.Theme.Name == themeName)
                .ToListAsync();
        }

        public async Task<TaskEntity?> UpdateAsyncTask(TaskEntity task)
        {
            context.TaskEntities.Update(task);
            await context.SaveChangesAsync();
            return task;

        }

      

        public async Task SaveAsync() => await context.SaveChangesAsync();
        

    }
}
