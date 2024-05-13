using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;
public class LearningSession
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Subject Subject { get; set; } = Subject.None;
    // Calculated in minutes
    public int Duration { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaximumLearners { get; set; }
    public Guid TutorId { get; set; }
    public Tutor Tutor { get; set; } = new Tutor();
    public List<Learner> EnrolledLearners { get; set; } = new List<Learner>();
}
