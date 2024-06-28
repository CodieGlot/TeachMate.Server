using TeachMate.Domain;

namespace TeachMate.Services;
public class ReportService : IReportService
{
    private readonly DataContext _context;

    public ReportService(DataContext context)
    {
        _context = context;
    }
    public async Task<Report> SentReportSystem(ReportSystemDto dto, AppUser user)
    {

        var reportSystem = new SystemReport()
        {
            SystemReportType = dto.SystemReportType,
        };
        var report = new Report 
        {
            UserID = user.Id,
            SystemReport = reportSystem,
            Title = dto.Title,
            Description = dto.Description,
        };

        _context.Add(report);
        await _context.SaveChangesAsync();

        return report;
    }

    public async Task<Report> SentReportUser(ReportUserDto dto, AppUser user)
    {
        var reportUser = new UserReport()
        {
            UserReportType = dto.UserReportType,
            ReportedUserId = dto.ReportedUserId,
        };
        var report = new Report
        {
            UserID = user.Id,
            UserReport = reportUser,
            Title = dto.Title,
            Description = dto.Description,
        };

        _context.Add(report);
        await _context.SaveChangesAsync();

        return report;
    }
}
