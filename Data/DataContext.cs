using Microsoft.EntityFrameworkCore;

namespace personal_trainer_api.Data
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
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<UserManagement> UserManagement { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutManagement> WorkoutManagement { get; set; }


        // Logging
        public DbSet<LoggingTrace> LoggingTrace { get; set; }
        public DbSet<LoggingException> LoggingException { get; set; }
        public DbSet<LoggingDataExchange> LoggingDataExchange { get; set; }
    }
}
