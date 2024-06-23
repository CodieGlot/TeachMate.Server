using Microsoft.AspNetCore.Http;
using TeachMate.Domain;

namespace TeachMate.Services;
public class HttpContextService : IHttpContextService
{
    private AppUser? _appUser;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public HttpContextService(IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public async Task<AppUser?> GetAppUser()
    {
        if (_appUser != null)
            return _appUser;

        var userId = GetUserId();

        if (userId != null)
        {
            _appUser = await _userService.GetUserById((Guid)userId);
        }

        return _appUser;
    }

    public async Task<AppUser> GetAppUserAndThrow()
    {
        return await GetAppUser() ?? throw new UnauthorizedException();
    }
    public string GetIpAddress()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            return string.Empty;
        }

        var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = context.Connection.RemoteIpAddress?.ToString();
        }
        return ipAddress ?? string.Empty;
    }
    private Guid? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

        return userId is not null
            ? new Guid(userId)
            : null;
    }
}
