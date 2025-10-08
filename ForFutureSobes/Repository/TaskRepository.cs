using ForFutureSobes.Interfaces;
using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ForFutureSobes.DTOs;

namespace ForFutureSobes.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ForFutureSobesDbContext _context;
        public TaskRepository(ForFutureSobesDbContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity?> CreateAsync(TaskEntity task, string themeName)
        {
            var theme = await _context.Themes.FirstOrDefaultAsync(x => x.Name == themeName);

            task.ThemeId = theme.Id;
            _context.TaskEntities.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteByThemeAsync(List<TaskEntity> tasks)
        {
            
            _context.TaskEntities.RemoveRange(tasks);
            await _context.SaveChangesAsync();  
            return true;
        }

        public async Task<List<TaskEntity>> GetAllAsync() => await _context.TaskEntities
                                                               .Include(x=>x.Theme)
                                                               .OrderBy(x=> x.IsCompleted)
                                                               .ToListAsync();




        public async Task<TaskEntity?> GetByIdAsync(int id) => await _context.TaskEntities
                                                                 .Include(x => x.Theme)
                                                                 .FirstOrDefaultAsync(x => x.Id == id);


        public async Task<List<TaskEntity>> GetByThemeAsync(string themeName) => await _context.TaskEntities
                                                                                   .Include(x => x.Theme)
                                                                                   .Where(x => x.Theme.Name == themeName)
                .ToListAsync();
        

        public async Task<TaskEntity?> UpdateAsyncTask(TaskEntity task)
        {
            _context.TaskEntities.Update(task);
            await _context.SaveChangesAsync();
            return task;

        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<List<TaskEntity>> GetAllUncompletedTasksAsync() =>  await _context.TaskEntities
                                                                                .Where(x => x.IsCompleted==false)
                                                                                .Include(x => x.Theme)
                                                                                .ToListAsync();

        public async Task<List<TaskEntity>> GetTasksByPriority(string priority) => await _context.TaskEntities
                                                                                     .Where(t => t.Priority.ToLower() == priority.ToLower())
                                                                                     .ToListAsync();
    }
}
