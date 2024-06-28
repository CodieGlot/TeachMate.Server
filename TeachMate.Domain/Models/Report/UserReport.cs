using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;

public class UserReport
{
    [Key]
    public int Id { get; set; }
    public AppUser ReportedUser { get; set; }
    public UserReportType UserReportType { get; set; }
    public Guid ReportedUserId { get; set; }
}
