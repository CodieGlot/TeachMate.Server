using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;

public class Report
{
    [Key]
    public int Id { get; set; }
    public Guid UserID { get; set; }
    public ReportSystem? ReportSystem { get; set; }
    public ReportUser? ReportUser { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Pending; 
}
