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

        public async Task<TaskEntity?> Create(TaskEntity task, string themeName)
        {
            var theme = await context.Themes.FirstOrDefaultAsync(x => x.Name == themeName);

            task.ThemeId = theme.Id;
            context.TaskEntities.Add(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteByTheme(List<TaskEntity> tasks)
        {
            
            context.TaskEntities.RemoveRange(tasks);
            await context.SaveChangesAsync();  
            return true;
        }

        public Task<List<TaskEntity>> GetAll() =>  context.TaskEntities
                                                              .Include(x=>x.Theme)
                                                              .ToListAsync();
        

        public  Task<TaskEntity?> GetById(int id) =>  context.TaskEntities
                                                                .Include(x => x.Theme)
                                                                .FirstOrDefaultAsync(x => x.Id == id);


        public  Task<List<TaskEntity>> GetByTheme(string themeName) =>  context.TaskEntities
                                                                          .Include(x => x.Theme)
                                                                          .Where(x => x.Theme.Name == themeName)
                                                                          .ToListAsync();
        

        public async Task<TaskEntity?> Update(TaskEntity task)
        {
            context.TaskEntities.Update(task);
            context.SaveChanges();
            return task;

        }

        public async Task Save() =>  context.SaveChanges();
        

    }
}
