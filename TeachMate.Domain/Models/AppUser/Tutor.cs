using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;
public class Tutor
{
    [Key]
    public Guid Id { get; set; }
    public List<LearningModule> CreatedModules { get; set; } = new List<LearningModule>();
    public AppUser AppUser { get; set; } = null!;
}
