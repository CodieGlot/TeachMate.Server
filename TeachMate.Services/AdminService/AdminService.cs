using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;

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

    public async Task<List<AppUser>> GetUserDisable(UserRole userrole)
    {
        var appUser = await _context.AppUsers
                            .Where(u => u.IsDisabled && u.UserRole == userrole)
                            .ToListAsync();

        foreach (var user in appUser)
        {
            Console.WriteLine(user);
        }
        return appUser;
    }
}
