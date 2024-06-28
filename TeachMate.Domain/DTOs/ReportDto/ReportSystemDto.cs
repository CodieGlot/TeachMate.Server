namespace TeachMate.Domain;

public class ReportSystemDto
{
    public SystemReportType SystemReportType { get; set; }
    public String Title { get; set; } = String.Empty;
    public String Description { get; set; } = String.Empty;
}
