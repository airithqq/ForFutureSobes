using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForFutureSobes.Data;
using ForFutureSobes.Domain;

namespace ForFutureSobes.Services
{
    public class ManageTaskService : ITaskService
    {
        private readonly ForFutureSobesDbContext _context;
        private readonly DbSet<TaskEntity> _dbSet;

        public ManageTaskService(ForFutureSobesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TaskEntity>();
        }
        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task, string themeName)
        {
            var theme = await _context.Themes.FirstOrDefaultAsync(x => x.Name == themeName);
            task.ThemeId = theme.Id;
            _context.TaskEntities.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity> GetTaskByIdAsync(int id, string themeName)
        {
            return await _context.TaskEntities
                .Include(x => x.Theme)
                .FirstOrDefaultAsync(x => x.Id == id && x.Theme.Name == themeName);
        }

        public async Task<List<TaskEntity>> GetTasksByThemeAsync(string themeName)
        {
            return await _context.TaskEntities
                 .Include(x => x.Theme)
                 .Where(x => x.Theme.Name == themeName)
                 .ToListAsync();
        }
        public async Task<bool> DeleteTaskAsync(int id, string themeName)
        {
            var task = await GetTaskByIdAsync(id, themeName);
            _context.TaskEntities.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
        public Task<bool> UpdateTaskAsync(int id, TaskEntity updatedTask, string themeName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskEntity>> GetAllTasksAsync()
        {
            return await _context.TaskEntities
                .Include(t => t.Theme)
                .ToListAsync();
        }
    }
}
