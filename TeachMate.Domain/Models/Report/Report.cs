using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;

public class Report
{
    [Key]
    public int Id { get; set; }
    public Guid UserID { get; set; }
    public AppUser User { get; set; }
    public SystemReport? SystemReport { get; set; }
    public UserReport? UserReport { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ReportStatus Status { get; set; } = ReportStatus.Pending; 
}
