using TeachMate.Domain;

namespace TeachMate.Services;

public interface IReportService
{
    Task<Report> SentReportSystem(ReportSystemDto dto, AppUser user);
    Task<Report> SentReportUser(ReportUserDto dto, AppUser user);
}
