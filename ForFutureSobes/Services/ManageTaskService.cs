using ForFutureSobes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForFutureSobes.Data;
using ForFutureSobes.Domain;
using AutoMapper;
using ForFutureSobes.DTOs;

namespace ForFutureSobes.Services
{
    public class ManageTaskService : ITaskService
    {
        private readonly ForFutureSobesDbContext _context;
        private readonly IMapper _mapper;

        public ManageTaskService(ForFutureSobesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
       
        }
        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task, string themeName)
        {
            var theme = await _context.Themes.FirstOrDefaultAsync(x => x.Name == themeName);
            if (theme == null) return null;
            
            task.ThemeId = theme.Id;

            _context.TaskEntities.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity> GetTaskByIdAsync(int id)
        {
            return await _context.TaskEntities
                .Include(x => x.Theme)
                .FirstOrDefaultAsync(x => x.Id == id );
        }

        public async Task<List<TaskEntity>> GetTasksByThemeAsync(string themeName)
        {
            return await _context.TaskEntities
                 .Include(x => x.Theme)
                 .Where(x => x.Theme.Name == themeName)
                 .ToListAsync();
        }
        public async Task<bool> DeleteTaskAsync(string themeName)
        {
            var task = await GetTasksByThemeAsync(themeName);
            foreach (var taskEntity in task) {
                _context.TaskEntities.Remove(taskEntity);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<TaskEntity> UpdateTaskAsync(int id, TaskEntity updatedTask, string themeName)
        {
            var task = await _context.TaskEntities.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null) return null;
            var theme = await _context.Themes.FirstOrDefaultAsync(t => t.Name == themeName);
            if (theme == null) return null;

            task.Description = updatedTask.Description;
            task.Priority = updatedTask.Priority;
            task.Title = updatedTask.Title;
            task.IsCompleted = updatedTask.IsCompleted;

            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<List<ResponseDTO>> GetAllTasksAsync()
        {
            var tasks = await _context.TaskEntities
            .Include(t => t.Theme)
            .ToListAsync();

            return _mapper.Map<List<ResponseDTO>>(tasks);
        }
    }
}
