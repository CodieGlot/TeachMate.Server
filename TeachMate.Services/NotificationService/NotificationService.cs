using IO.Ably;
using IO.Ably.Realtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TeachMate.Domain;

namespace TeachMate.Services;
public class NotificationService : INotificationService
{
    private readonly AblyRealtime ably;
    private readonly DataContext _context;
    public NotificationService(IOptions<AblyConfig> ablyConfig, DataContext context)
    {
        ably = new AblyRealtime(ablyConfig.Value.ApiKey);
        ably.Connection.On(ConnectionEvent.Connected, args =>
        {
            Console.Out.WriteLine("Connected to Ably!");
        });
        _context = context;
    }
    public async Task<List<PushNotification>> GetLatestNotifications(AppUser user)
    {
        return await _context.PushNotificationReceivers
            .Where(pr => pr.ReceiverId == user.Id)
            .OrderByDescending(pr => pr.PushNotification.CreatedAt)
            .Take(4)
            .Select(pr => pr.PushNotification)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<PushNotification> CreatePushNotification(NotificationType type, AppUser? creator, List<Guid> receiverIds, List<object> messageParams)
    {
        var pushNotification = new PushNotification
        {
            Type = type,
            Message = type.ToApiDescription(messageParams)
        };

        if (creator != null)
        {
            pushNotification.CreatorId = creator.Id;
            pushNotification.CreatorDisplayName = creator.DisplayName;
        }

        pushNotification.Receivers = receiverIds.Select(id => new PushNotificationReceiver { ReceiverId = id }).ToList();

        _context.Add(pushNotification);
        await _context.SaveChangesAsync();

        await SendPushNotifications(receiverIds, pushNotification);

        return pushNotification;
    }
    private async Task SendPushNotifications(List<Guid> receiverIds, PushNotification pushNotification)
    {
        if (receiverIds == null || receiverIds.Count == 0) return;

        pushNotification.Receivers = new List<PushNotificationReceiver>();

        var tasks = new List<Task>();

        foreach (var receiverId in receiverIds)
        {
            var channel = ably.Channels.Get($"Notification:{receiverId}");
            tasks.Add(channel.PublishAsync("Notification", pushNotification));
        }

        await Task.WhenAll(tasks);
    }
}
