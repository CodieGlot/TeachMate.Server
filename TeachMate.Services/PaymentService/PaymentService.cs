using TeachMate.Domain;

namespace TeachMate.Services;
public class PaymentService : IPaymentService
{
    private readonly IZaloPayService _zaloPayService;
    private readonly IMomoService _momoService;

    public PaymentService(IZaloPayService zaloPayService, IMomoService momoService)
    {
        _zaloPayService = zaloPayService;
        _momoService = momoService;
    }
    public async Task<OrderUrlResponseDto> CreateOrderUrl(double amount, PaymentProviderType type)
    {
        return type switch
        {
            PaymentProviderType.ZaloPay => await _zaloPayService.CreateZaloPayOrder(amount),
            PaymentProviderType.Momo => await _momoService.CreateMomoOrder(amount),
            _ => throw new NotImplementedException(),
        };
    }
}
