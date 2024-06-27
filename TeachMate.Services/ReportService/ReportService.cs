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

        var reportSystem = new ReportSystem()
        {
            typeErrorSystem = dto.typeErrorSystem,
        };
        var report = new Report 
        {
            UserID = user.Id,
            ReportSystem = reportSystem,
            Title = dto.Title,
            Description = dto.Description,
        };

        _context.Add(report);
        await _context.SaveChangesAsync();

        return report;
    }

    public async Task<Report> SentReportUser(ReportUserDto dto, AppUser user)
    {
        var reportUser = new ReportUser()
        {
            typeErrorUser = dto.typeErrorUser,
            UserIdReported = dto.UserIDReported,
        };
        var report = new Report
        {
            UserID = user.Id,
            ReportUser = reportUser,
            Title = dto.Title,
            Description = dto.Description,
        };

        _context.Add(report);
        await _context.SaveChangesAsync();

        return report;
    }
}
