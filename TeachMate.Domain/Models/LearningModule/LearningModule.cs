using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TeachMate.Domain;

namespace TeachMate.Domain;
public class LearningModule
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Subject Subject { get; set; } = Subject.None;
    // Calculated in minutes
    public int GradeLevel { get; set; }
    public int Duration { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    /*[NotMapped]
    public List<LearningSession> Schedule { get; set; } = new();
    [JsonIgnore]
    public string SerializedSchedule { get; set; } = string.Empty;*/
    public List<LearningSession> Schedule { get; set; } = new();
    public int MaximumLearners { get; set; }
    public Guid TutorId { get; set; }
    [JsonIgnore]
    public Tutor Tutor { get; set; } = new Tutor();
    public List<Learner> EnrolledLearners { get; set; } = new List<Learner>();
    public ModuleType ModuleType { get; set; }
    public int NumOfWeeks { get; set; }
    public double Price { get; set; } = 0;

    public PaymentType PaymentType { get; set; } = PaymentType.Session;
    public WeeklySchedule? WeeklySchedule { get; set; }

    public List<LearningModulePaymentOrder> LearningModulePaymentOrder { get; set; } = new List<LearningModulePaymentOrder>();
    
    public List<LearningModuleRequest> LearningModuleRequests { get; set; } = new List<LearningModuleRequest>();

    public List<LearningChapter> LearningChapters { get; set; } = new List<LearningChapter> { };

    public List<LearningModuleFeedback> LearningModuleFeedback { get; set; } = new List<LearningModuleFeedback> { };
}
