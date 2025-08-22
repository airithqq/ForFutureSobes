
using ForFutureSobes.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForFutureSobes.Data
{
    public class ForFutureSobesDbContext: DbContext
    {
        public ForFutureSobesDbContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
        { 
        }

        public DbSet<Aspnet> Aspnet { get; set; }

        public DbSet<EntityFW> Efw { get; set; }

        public DbSet<Program_language> Pl { get; set; }

        public DbSet<Sqlth> Sql_th { get; set; }
    }
}
