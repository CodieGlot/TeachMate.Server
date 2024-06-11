using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [HttpPost("zalopay")]
    public async Task<ActionResult<ResponseDto>> CreateZaloPayOrder()
    {
        return await _paymentService.TestZaloPay();
    }
    [HttpPost("momo")]
    public async Task<ActionResult<ResponseDto>> CreateMomoOrder()
    {
        return await _paymentService.TestMomo();
    }
}
