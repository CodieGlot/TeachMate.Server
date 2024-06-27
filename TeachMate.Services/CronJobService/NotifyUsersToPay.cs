using Quartz;
using TeachMate.Domain;

namespace TeachMate.Services;
public class NotifyUsersToPay : IJob
{
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    private readonly INotificationService _notificationService;

    public NotifyUsersToPay(IUserService userService, IEmailService emailService, INotificationService notificationService)
    {
        _userService = userService;
        _emailService = emailService;
        _notificationService = notificationService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var user = await _userService.GetUserByUsernameOrEmail("lpdmy15@gmail.com");

        if (user != null)
        {
            _emailService.SendTestEmail(user);

            await _notificationService.CreatePushNotification(NotificationType.PaymentRequired, null, new List<Guid> { user.Id }, new List<object>());
        }
    }
}
