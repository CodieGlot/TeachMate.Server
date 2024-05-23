using IO.Ably;
using IO.Ably.Realtime;
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
    public async void SendTestNotification()
    {
        var channel = ably.Channels.Get("Test");

        await channel.PublishAsync("Notification", "Test message");
    }
}
