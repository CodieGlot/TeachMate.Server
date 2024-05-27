using TeachMate.Domain;
using TeachMate.Domain.DTOs.InformationDto;

namespace TeachMate.Services;
public interface IUserService
{
    Task<AppUser> CreateUser(AppUser appUser);
    Task<AppUser?> DisableUser(Guid id);
    Task<AppUser?> GetUserById(Guid id);
    Task<AppUser?> GetUserByUsernameOrEmail(string usernameOrEmail);
    Task<AppUser> UpdateUser(AppUser appUser);
    Task<AppUser> ChangeUserPassWord(AppUser user, UserPassword dto);
}