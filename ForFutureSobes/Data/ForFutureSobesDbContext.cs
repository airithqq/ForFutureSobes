
using ForFutureSobes.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForFutureSobes.Data
{
    public class ForFutureSobesDbContext : DbContext
    {
        public ForFutureSobesDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<TaskEntity> TaskEntities {get;set;}

        public DbSet<Theme> Themes { get; set; }
    }
}
