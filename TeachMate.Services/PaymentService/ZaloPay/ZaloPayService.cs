using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using TeachMate.Domain;

public class ZaloPayService : IZaloPayService
{
    private readonly ZaloPayConfig _zaloPayConfig;
    public ZaloPayService(IOptions<ZaloPayConfig> zaloPayConfig)
    {
        _zaloPayConfig = zaloPayConfig.Value;
    }
    public async Task<OrderUrlResponseDto> CreateZaloPayOrder(double amount)
    {
        Random rnd = new Random();
        var items = Array.Empty<object>();
        var embed_data = new { };
        var app_trans_id = rnd.Next(1000000); // Generate a random order's ID.

        var param = new Dictionary<string, string>
        {
            { "app_id", _zaloPayConfig.AppId },
            { "app_user", "teachmate_user" },
            { "app_time", GetUnixTimestamp().ToString() },
            { "amount", amount.ToString() },
            { "app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id }, // the trading code must be in the format yymmdd_xxxx
            { "embed_data", JsonConvert.SerializeObject(embed_data) },
            { "item", JsonConvert.SerializeObject(items) },
            { "description", "TeachMate - Order #" + app_trans_id },
            { "bank_code", "zalopayapp" }
        };

        var data = _zaloPayConfig.AppId + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|"
            + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
        param.Add("mac", ComputeHmacSha256(_zaloPayConfig.Key1, data));

        var result = await PostFormAsync(_zaloPayConfig.CreateOrderUrl, param);

        return result["return_code"] == "1"
            ? new OrderUrlResponseDto(result["order_url"])
            : throw new ExternalServiceException("Momo Service not working.");
    }

    private long GetUnixTimestamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    private string ComputeHmacSha256(string key, string data)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    private async Task<Dictionary<string, string>> PostFormAsync(string url, Dictionary<string, string> parameters)
    {
        using (var client = new HttpClient())
        {
            var content = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString) ?? new Dictionary<string, string>();
        }
    }
}
