using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;
public class Learner
{
    [Key]
    public Guid Id { get; set; }
    public List<LearningModule> EnrolledModules { get; set; } = new List<LearningModule>();
    public AppUser AppUser { get; set; } = null!;
}
