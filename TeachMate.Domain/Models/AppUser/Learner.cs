using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;
public class Learner
{
    [Key]
    public Guid Id { get; set; }
    public List<LearningSession> EnrolledSessions { get; set; } = new List<LearningSession>();
    public AppUser AppUser { get; set; } = null!;
}
