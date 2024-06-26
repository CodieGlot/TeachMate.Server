using TeachMate.Domain;

namespace TeachMate.Services;
public class PaymentService : IPaymentService
{
    private readonly IZaloPayService _zaloPayService;
    private readonly IMomoService _momoService;
    private readonly IVnPayService _vnPayService;

    public PaymentService(IZaloPayService zaloPayService, IMomoService momoService, IVnPayService vnPayService)
    {
        _zaloPayService = zaloPayService;
        _momoService = momoService;
        _vnPayService = vnPayService;
    }
    public async Task<OrderUrlResponseDto> CreateOrderUrl(double amount, PaymentProviderType type)
    {
        return type switch
        {
            PaymentProviderType.ZaloPay => await _zaloPayService.CreateZaloPayOrder(amount),
            PaymentProviderType.Momo => await _momoService.CreateMomoOrder(amount),
            PaymentProviderType.VnPay => _vnPayService.CreateVnPayOrder(amount),
            _ => throw new NotImplementedException(),
        };
    }

    public Task<LearningModule> SetPriceForLearningModuleDto(SetPriceForLearningModuleDto dto)
    {
        throw new NotImplementedException();
    }
}
