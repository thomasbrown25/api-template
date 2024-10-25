using Microsoft.EntityFrameworkCore;

namespace template_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // seed the skill data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Role)
                .HasConversion<string>();

        }

        public DbSet<User> Users { get; set; }


        // Logging
        public DbSet<LoggingTrace> LoggingTrace { get; set; }
        public DbSet<LoggingException> LoggingException { get; set; }
        public DbSet<LoggingDataExchange> LoggingDataExchange { get; set; }
    }
}
