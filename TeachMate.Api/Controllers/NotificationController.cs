using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api.Controllers;
[Authorize(Roles = CustomRoles.GeneralUser)]
[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IHttpContextService _contextService;
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService, IHttpContextService contextService)
    {
        _notificationService = notificationService;
        _contextService = contextService;
    }
    [HttpGet("Latest")]
    public async Task<ActionResult<List<PushNotification>>> GetLatestNotification()
    {
        var user = await _contextService.GetAppUserAndThrow();
        return Ok(await _notificationService.GetLatestNotifications(user));
    }
}
