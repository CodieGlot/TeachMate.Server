using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using TeachMate.Domain;

namespace TeachMate.Services;
public class MomoService : IMomoService
{
    private readonly MomoConfig _momoConfig;
    public MomoService(IOptions<MomoConfig> momoConfig)
    {
        _momoConfig = momoConfig.Value;
    }
    public async Task<string> MomoTest()
    {
        Guid myuuid = Guid.NewGuid();
        string myuuidAsString = myuuid.ToString();

        string accessKey = _momoConfig.AccessKey;
        string secretKey = _momoConfig.SecretKey;

        var request = new MomoCollectionLinkRequest
        {
            orderInfo = "pay with MoMo",
            partnerCode = "MOMO",
            redirectUrl = "https://webhook.site/b3088a6a-2d17-4f8d-a383-71389a6c600b",
            ipnUrl = "https://webhook.site/b3088a6a-2d17-4f8d-a383-71389a6c600b",
            amount = 5000,
            orderId = myuuidAsString,
            requestId = myuuidAsString,
            requestType = "payWithMethod",
            extraData = "",
            partnerName = "MoMo Payment",
            storeId = "Test Store",
            orderGroupId = "",
            autoCapture = true,
            lang = "en"
        };

        var rawSignature = $"accessKey={accessKey}&amount={request.amount}&extraData={request.extraData}&ipnUrl={request.ipnUrl}&orderId={request.orderId}&orderInfo={request.orderInfo}&partnerCode={request.partnerCode}&redirectUrl={request.redirectUrl}&requestId={request.requestId}&requestType={request.requestType}";
        request.signature = GetSignature(rawSignature, secretKey);

        using (var client = new HttpClient())
        {
            StringContent httpContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var quickPayResponse = await client.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", httpContent);
            var contents = await quickPayResponse.Content.ReadAsStringAsync();

            Console.WriteLine(contents);
            return contents;
        }
    }

    private static string GetSignature(string text, string key)
    {
        var encoding = new ASCIIEncoding();
        var textBytes = encoding.GetBytes(text);
        var keyBytes = encoding.GetBytes(key);
        byte[] hashBytes;

        using (var hash = new HMACSHA256(keyBytes))
            hashBytes = hash.ComputeHash(textBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
