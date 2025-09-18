using Microsoft.EntityFrameworkCore;
using WorkScheduler.Shared;



namespace WorkScheduler.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Shift> Shifts { get; set;} = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Shift>().HasKey(s => s.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
