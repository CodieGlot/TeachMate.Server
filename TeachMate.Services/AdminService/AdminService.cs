﻿using Microsoft.EntityFrameworkCore;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;

namespace TeachMate.Services;

public class AdminService : IAdminService
{
    private readonly DataContext _context;

    public AdminService(DataContext context)
    {
        _context = context;
    }
    public async Task<List<AppUser>> GetAllUser()
    {
        var appUser = await _context.AppUsers
            .Where(user => user.UserRole != UserRole.Admin)
            .ToListAsync();

        foreach (var user in appUser)
        {
            Console.WriteLine(user);
        }
        return appUser;
    }

    public async Task<List<AppUser>> SearchUser(SearchUserDto dto)
    {
        var query = _context.AppUsers.AsQueryable()
                .Where(u => u.UserRole != UserRole.Admin);

        if (!string.IsNullOrWhiteSpace(dto.DisplayNameOrUsername))
        {
            var trimmedSearchTerm = dto.DisplayNameOrUsername.Trim().ToLower();
            query = query.Where(m => EF.Functions.Like(m.DisplayName.ToLower(), $"%{trimmedSearchTerm}%") ||
                                     EF.Functions.Like(m.Username.ToLower(), $"%{trimmedSearchTerm}%"));
        }

        if (dto.UserRole != null)
        {
            query = query.Where(m => m.UserRole == dto.UserRole);
        }

        if (dto.IsDisable.HasValue)
        {
            query = query.Where(m => m.IsDisabled == dto.IsDisable);
        }
        return await query.ToListAsync();
    }

    public async Task<AppUser?> UpdateStatus(DisableDto dto)
    {
        var appUser = await _context.AppUsers
            .Where(u => u.UserRole != UserRole.Admin)
            .FirstOrDefaultAsync(u => u.Id == dto.Id);

        if (appUser != null)
        {
            appUser.IsDisabled = !appUser.IsDisabled;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new NotFoundException("Not found");
        }

        return appUser;
    }

    public async Task<Report?> GetReportByID(int id)
    {

        var report = await _context.Report
            .FirstOrDefaultAsync(x => x.Id == id);

        return report;
    }

    public async Task<List<Report>> GetAllReportSystem()
    {
        var report = await _context.Report
            .Where(r => r.SystemReport != null)
            .Include(r => r.SystemReport)
            .Include(r => r.User)
            .ToListAsync();

        foreach (var r in report)
        {
            Console.WriteLine(r);
        }
        return report;
    }

    public async Task<List<Report>> GetAllReportUser()
    {
        var report = await _context.Report
            .Where(r => r.UserReport != null)
            .Include(r => r.UserReport.ReportedUser) // Sử dụng eager loading để nạp ReportUser
            .Include (r => r.User)
            .ToListAsync();

        foreach (var r in report)
        {
            Console.WriteLine(r);
        }
        return report;
    }

    public async Task<List<Report>> SearchReportSystem(SearchReportSystemDto dto)
    {
        var query = _context.Report
            .Where(r => r.SystemReport !=  null)
            .Include(r => r.SystemReport)
            .Include(r => r.User)
            .AsQueryable();

        if (dto.SystemReportType != null)
        {
            query = query.Where(m => m.SystemReport.SystemReportType == dto.SystemReportType);
        }
        if (dto.reportStatus != null)
        {
            query = query.Where(m => m.Status == dto.reportStatus);
        }
        return await query.ToListAsync();
    }

    public async Task<List<Report>> SearchReportUser(SearchReportUserDto dto)
    {
        var query = _context.Report
            .Where (r => r.UserReport != null)
            .Include(r => r.UserReport.ReportedUser)
            .Include(r => r.User)
            .AsQueryable();

        if (dto.UserReportType != null)
        {
            query = query.Where(m =>m.UserReport.UserReportType == dto.UserReportType);
        }
        if (dto.reportStatus != null)
        {
            query = query.Where(m =>m.Status == dto.reportStatus);
        }
        return await query.ToListAsync();
    }

    public async Task<Report?> UpdateStatusReport(UpdateStatusDto dto)
    {
        var report = await _context.Report
            .FirstOrDefaultAsync(r => r.Id == dto.Id);

        if (report != null)
        {
            report.Status = dto.ReportStatus;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new NotFoundException("Not found");
        }

        return report;
    }
}
