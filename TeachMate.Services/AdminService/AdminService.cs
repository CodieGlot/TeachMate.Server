using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        appUser.ForEach(user => Console.WriteLine(user));
        foreach (var user in appUser)
        {
            Console.WriteLine(user);
        }
        return appUser;
    }

    public async Task<List<AppUser>> SearchUser(SearchUserDto dto)
    {
        var query = _context.AppUsers.AsQueryable()
                .Where(u =>u.UserRole != UserRole.Admin);

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
}
