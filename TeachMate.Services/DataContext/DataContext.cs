using Microsoft.EntityFrameworkCore;
using TeachMate.Domain;
using TeachMate.Domain.Models.Schedule;

namespace TeachMate.Services;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>()
            .HasOne(x => x.Tutor)
            .WithOne(x => x.AppUser)
            .HasForeignKey<Tutor>();
        modelBuilder.Entity<AppUser>()
            .HasOne(x => x.Learner)
            .WithOne(x => x.AppUser)
            .HasForeignKey<Learner>();
        modelBuilder.Entity<LearningModule>()   
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<LearningModuleRequest>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Learner> Learners { get; set; }
    public DbSet<LearningModule> LearningModules { get; set; }
    public DbSet<LearningModuleRequest> LearningModuleRequests { get; set; }

    public DbSet<WeeklySchedule> WeeklySchedules { get; set;}
    public DbSet<WeeklySlot> WeeklySlots { get; set; }
    public DbSet<LearningSession> LearningSessions { get; set; }
}
