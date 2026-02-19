using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexties
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Log> Logs { get; set; }       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Log>().HasKey("Id");

        }

        

       
    }
}
