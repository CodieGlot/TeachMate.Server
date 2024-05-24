using TeachMate.Domain;

namespace TeachMate.Services;
public interface INotificationService
{
    Task<PushNotification> CreatePushNotification(PushNotificationType type, AppUser? creator, List<Guid> receiverIds, List<object> messageParams);
    Task<List<PushNotification>> GetLatestNotifications(AppUser user);
}