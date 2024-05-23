namespace TeachMate.Domain;
public class PushNotificationReceiver
{
    public int PushNotificationId { get; set; }
    public Guid ReceiverId { get; set; }
    public PushNotification PushNotification { get; set; } = null!;
}
