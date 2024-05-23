using System.ComponentModel.DataAnnotations;

namespace TeachMate.Domain;
public class PushNotification
{
    [Key]
    public int Id { get; set; }
    public PushNotificationType Type { get; set; }
    public Guid CreatorId { get; set; } = Guid.Empty;
    public string CreatorDisplayName { get; set; } = string.Empty;
    public string CustomMessage { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public List<PushNotificationReceiver> Receivers { get; set; } = new();
}
