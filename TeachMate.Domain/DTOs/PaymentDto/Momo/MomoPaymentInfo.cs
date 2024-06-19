using System.Text.Json.Serialization;

namespace TeachMate.Domain;
public class MomoPaymentInfo
{
    [JsonPropertyName("partnerCode")]
    public string PartnerCode { get; set; }

    [JsonPropertyName("orderId")]
    public Guid OrderId { get; set; }

    [JsonPropertyName("requestId")]
    public Guid RequestId { get; set; }

    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("responseTime")]
    public long ResponseTime { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("resultCode")]
    public int ResultCode { get; set; }

    [JsonPropertyName("payUrl")]
    public string PayUrl { get; set; }

    [JsonPropertyName("shortLink")]
    public string ShortLink { get; set; }
}