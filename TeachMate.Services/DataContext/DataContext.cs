using Microsoft.EntityFrameworkCore;
using TeachMate.Domain;

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
        modelBuilder.Entity<PushNotification>()
            .HasIndex(x => x.CreatedAt);
        modelBuilder.Entity<PushNotificationReceiver>()
            .HasKey(x => new { x.PushNotificationId, x.ReceiverId });


        modelBuilder.Entity<AppUser>().HasIndex(x => x.Email).IsUnique();


        modelBuilder.Entity<LearningChapter>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<LearningMaterial>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

 

        modelBuilder.Entity<UserReport>()
       .HasOne(ru => ru.ReportedUser)
       .WithMany()
       .HasForeignKey(ru => ru.ReportedUserId)
       .IsRequired()
       .OnDelete(DeleteBehavior.Cascade); // Adjust DeleteBehavior as needed
    }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Learner> Learners { get; set; }
    public DbSet<LearningModule> LearningModules { get; set; }
    public DbSet<LearningModuleRequest> LearningModuleRequests { get; set; }
    public DbSet<WeeklySchedule> WeeklySchedules { get; set; }
    public DbSet<WeeklySlot> WeeklySlots { get; set; }
    public DbSet<LearningSession> LearningSessions { get; set; }
    public DbSet<PushNotification> PushNotifications { get; set; }
    public DbSet<PushNotificationReceiver> PushNotificationReceivers { get; set; }

    public DbSet<LearningModuleFeedback> LearningModuleFeedbacks { get; set; }
    public DbSet<Like> Likes { get; set; }

    public DbSet<Dislike> Dislikes { get; set; }

    public object Users { get; set; }
    public DbSet<UserOTP> UserOTPs { get; set; }

    public DbSet<LearningModulePaymentOrder> LearningModulePaymentOrders { get; set; }
    public DbSet<Report> Report { get; set; }

    public DbSet<TutorReplyFeedback> TutorReplyFeedback { get; set; }
    public DbSet<SystemReport> SystemReports { get; set; }
    public DbSet<UserReport> UserReports { get; set; }
    public DbSet<LearningChapter> LearningChapters { get; set; }
    public DbSet<LearningMaterial> LearningMaterials { get; set; }
}
