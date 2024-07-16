using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TeachMate.Domain.Models.Certificate;

namespace TeachMate.Domain;
public class Tutor
{
    [Key]
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public List<LearningModule> CreatedModules { get; set; } = new List<LearningModule>();
    [JsonIgnore]
    public AppUser AppUser { get; set; } = null!;
    public string Description {  get; set; } = string.Empty;
    public int GradeLevel { get; set; }
    public List<Certificate> Certificates { get; set; } = new List<Certificate>();
}
