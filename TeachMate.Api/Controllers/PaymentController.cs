using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<OrderUrlResponseDto>> CreateZaloPayOrder()
    {
        return await _paymentService.CreateOrderUrl(50002, PaymentProviderType.ZaloPay);
    }
    [HttpPost("momo")]
    public async Task<ActionResult<OrderUrlResponseDto>> CreateMomoOrder()
    {
        return await _paymentService.CreateOrderUrl(50002, PaymentProviderType.Momo);
    }
    [HttpPost("vnpay")]
    public async Task<ActionResult<OrderUrlResponseDto>> CreateVnPayOrder()
    {
        return await _paymentService.CreateOrderUrl(50002, PaymentProviderType.VnPay);
    }
    [HttpPost("CreatePaymentOrder")]
    public async Task<ActionResult<LearningModulePaymentOrder>> CreatePaymentOrder(CreateOrderPaymentDto dto)
    { 
      return await _paymentService.CreatePaymentOrder(dto);
    }
    [HttpPut("PayForClass")]
    public async Task<ActionResult<ResponseDto>> PayForClass(int id) {
        
        return await _paymentService.PayForClass(id);
    }

    /// <summary>
    /// Set price for learning module
    /// </summary>
    /// <param name="dto">DTO containing LearningModuleId, Price, and PaymentType</param>
    /// <returns>Action result indicating success or failure</returns>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPut("SetPriceForLearningModule")]
    public async Task<ActionResult<LearningModule>> SetPriceForLearningModule(SetPriceForLearningModuleDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedModule = await _paymentService.SetPriceForLearningModule(dto);

            if (updatedModule == null)
            {
                return NotFound($"Learning module with ID {dto.LearningModuleId} not found.");
            }

            return Ok(updatedModule);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}
