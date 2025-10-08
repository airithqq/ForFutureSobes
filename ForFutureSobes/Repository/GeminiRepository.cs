using ForFutureSobes.Data;
using ForFutureSobes.Interfaces;
using Microsoft.EntityFrameworkCore;
using ForFutureSobes.Domain;

namespace ForFutureSobes.Repository
{
    public class GeminiRepository : IGeminiRepository
    {
        private readonly ForFutureSobesDbContext _context;
        public GeminiRepository(ForFutureSobesDbContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity?> GetPromptFromTask(int taskId)
        {
            return await _context.TaskEntities
                     .Include(t => t.Theme)
                     .Where(t => t.IsCompleted==false)
                     .FirstOrDefaultAsync(t => t.Id == taskId);
        }
    }
}
