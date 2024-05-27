
using TeachMate.Domain;

namespace TeachMate.Services;

public interface IAdminService
{
    Task<List<AppUser>> GetAllUser();
    Task<List<AppUser>> GetUserDisable(UserRole userRole);
}
