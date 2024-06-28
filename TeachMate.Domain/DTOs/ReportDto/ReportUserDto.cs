namespace TeachMate.Domain;

public class ReportUserDto
{
    public UserReportType UserReportType { get; set; }
    public String Title { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
    public Guid ReportedUserId { get; set; }
}
