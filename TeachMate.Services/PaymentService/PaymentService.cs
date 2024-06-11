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
    public async Task<ResponseDto> TestZaloPay()
    {
        var result = await _zaloPayService.TestZaloPay();
        return new ResponseDto(result);
    }
    public async Task<ResponseDto> TestMomo()
    {
        var result = await _momoService.MomoTest();
        return new ResponseDto(result);
    }
}
