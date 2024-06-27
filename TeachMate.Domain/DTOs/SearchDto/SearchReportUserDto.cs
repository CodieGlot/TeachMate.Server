namespace TeachMate.Domain;

public class SearchReportUserDto
{
    public UserReportType? typeErrorUser { get; set; } = null;
    public ReportStatus? status { get; set; } = null;
}
