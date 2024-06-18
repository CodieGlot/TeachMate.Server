using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;
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
    /// Get all user
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("GetAllUser")]
    public async Task<ActionResult<List<AppUser>>> GetAllUser()
    {
        return Ok(await _adminService.GetAllUser());
    }

    /// <summary>
    /// Search User
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPost("SearchUser")]
    public async Task<ActionResult<List<AppUser>>> SearchUser(SearchUserDto dto)
    {
        return Ok(await _adminService.SearchUser(dto));
    }

    /// <summary>
    /// Disable user
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPut("DisableUser")]
    public async Task<ActionResult<AppUser>> DisableUser(Guid Id)
    {
        return Ok(await _userService.DisableUser(Id));
    }


}
