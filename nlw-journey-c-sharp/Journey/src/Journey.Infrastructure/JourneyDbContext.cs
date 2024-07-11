using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure
{
    public class JourneyDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO usar o caminho completo do JourneyDatabase.db
            optionsBuilder.UseSqlite("Data Source=JourneyDatabase.db");
        }
    }
}
