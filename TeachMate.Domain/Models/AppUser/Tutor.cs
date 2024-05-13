using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;
public class Tutor
{
    [Key]
    public Guid Id { get; set; }
    public List<LearningSession> CreatedSessions { get; set; } = new List<LearningSession>();
    public AppUser AppUser { get; set; } = null!;
}
