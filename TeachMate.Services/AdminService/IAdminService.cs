
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;

namespace TeachMate.Services;

public interface IAdminService
{
    Task<List<AppUser>> GetAllUser();
    Task<List<AppUser>> SearchUser(SearchUserDto dto);
    Task<AppUser?> UpdateStatus(DisableDto dto);
}
