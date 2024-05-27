using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAdminService _adminService;   

    public AdminController(IUserService userService, IAdminService adminService)
    {
        _userService = userService;
        _adminService = adminService;
    }

    /// <summary>
    /// Disable user
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPost("DisableUser")]
    public async Task<ActionResult<AppUser>> DisableUser(Guid Id)
    {
        return Ok(await _userService.DisableUser(Id));
    }

    /// <summary>
    /// List All User
    /// </summary>
    [Authorize(Roles =CustomRoles.Admin)]
    [HttpPost("List all user")]
    public async Task<ActionResult<AppUser>> ListUser()
    {
        return Ok(await _adminService.GetAllUser());
    }
}
