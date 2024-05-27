using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeachMate.Domain;
public class PushNotification
{
    [Key]
    public int Id { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid CreatorId { get; set; } = Guid.Empty;
    public string CreatorDisplayName { get; set; } = string.Empty;
    public string CustomMessage { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [JsonIgnore]
    public List<PushNotificationReceiver> Receivers { get; set; } = new();
}
