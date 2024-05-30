using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api.Controllers;
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
    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpGet("Latest")]
    public async Task<ActionResult<List<PushNotification>>> GetLatestNotification()
    {
        var user = await _contextService.GetAppUserAndThrow();
        return Ok(await _notificationService.GetLatestNotifications(user));
    }
    [HttpPost]
    public async Task<ActionResult<PushNotification>> TestCreate()
    {
        return Ok(await _notificationService.CreatePushNotification(NotificationType.NewLearningRequest, null, new List<Guid> { new Guid("932CF168-B52B-4E1B-74A7-08DC7DF8DF7F") }, new List<object> { "Codie Test" }));
    }
}
