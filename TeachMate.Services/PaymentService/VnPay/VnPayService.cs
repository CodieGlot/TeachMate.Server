using Microsoft.Extensions.Options;
using TeachMate.Domain;

namespace TeachMate.Services;
public class VnPayService : IVnPayService
{
    private readonly VnPayConfig _vnPayConfig;
    private readonly IHttpContextService _contextService;
    public VnPayService(IOptions<VnPayConfig> vnPayConfig, IHttpContextService contextService)
    {
        _vnPayConfig = vnPayConfig.Value;
        _contextService = contextService;

    }
    public OrderUrlResponseDto CreateVnPayOrder(double amount)
    {
        Guid orderId = Guid.NewGuid();
        var tick = DateTime.Now.Ticks.ToString();

        var vnpay = new VnPayLibrary();
        vnpay.AddRequestData("vnp_Version", _vnPayConfig.Version);
        vnpay.AddRequestData("vnp_Command", _vnPayConfig.Command);
        vnpay.AddRequestData("vnp_TmnCode", _vnPayConfig.TmnCode);
        vnpay.AddRequestData("vnp_Amount", (amount * 100).ToString());

        vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
        vnpay.AddRequestData("vnp_CurrCode", _vnPayConfig.CurrCode);
        vnpay.AddRequestData("vnp_IpAddr", _contextService.GetIpAddress());
        vnpay.AddRequestData("vnp_Locale", _vnPayConfig.Locale);

        vnpay.AddRequestData("vnp_OrderInfo", "Pay for Order #:" + orderId);
        vnpay.AddRequestData("vnp_OrderType", "other");
        vnpay.AddRequestData("vnp_ReturnUrl", _vnPayConfig.PaymentBackReturnUrl);

        vnpay.AddRequestData("vnp_TxnRef", tick);

        var paymentUrl = vnpay.CreateRequestUrl(_vnPayConfig.BaseUrl, _vnPayConfig.HashSecret);

        return new OrderUrlResponseDto(paymentUrl);
    }

    //public VnPaymentResponseModel PaymentExecute(IQueryCollection collections)
    //{
    //    var vnpay = new VnPayLibrary();
    //    foreach (var (key, value) in collections)
    //    {
    //        if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
    //        {
    //            vnpay.AddResponseData(key, value.ToString());
    //        }
    //    }

    //    var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
    //    var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
    //    var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
    //    var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
    //    var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

    //    bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
    //    if (!checkSignature)
    //    {
    //        return new VnPaymentResponseModel
    //        {
    //            Success = false
    //        };
    //    }

    //    return new VnPaymentResponseModel
    //    {
    //        Success = true,
    //        PaymentMethod = "VnPay",
    //        OrderDescription = vnp_OrderInfo,
    //        OrderId = vnp_orderId.ToString(),
    //        TransactionId = vnp_TransactionId.ToString(),
    //        Token = vnp_SecureHash,
    //        VnPayResponseCode = vnp_ResponseCode
    //    };
    //}
}
