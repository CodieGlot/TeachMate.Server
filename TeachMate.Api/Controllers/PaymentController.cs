﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IHttpContextService _contextService;
    public PaymentController(IPaymentService paymentService , IHttpContextService contextService)
    {
        _paymentService = paymentService;
        _contextService = contextService;
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
    public async Task<ActionResult<ResponseDto>> PayForClass(int OrderID) {
        
        return await _paymentService.PayForClass(OrderID);
    }
    [HttpPost("GetAllPaymentByLearnerID")]
    public async Task<ActionResult<List<LearningModulePaymentOrder>>> GetAllPaymentOrder() {
        var user = await _contextService.GetAppUserAndThrow();

        return await _paymentService.GetAllPaymentOrder(user.Id);
             
    }
    [HttpPost("GetAllPaymentByModuleID")]
    public async Task<ActionResult<List<LearningModulePaymentOrder>>> GetAllPaymentOrderByModuleId(int moduleId) {
        return await _paymentService.GetAllPaymentOrderByModuleID(moduleId);
    }

    [HttpPost("GetAllUnPaidByModuleIdByLearnerId")]
    public async Task<ActionResult<LearningModulePaymentOrder>> GetUnPaidPaymentOrderByModuleId(int moduleId) {
        var user = await _contextService.GetAppUserAndThrow();
        return await _paymentService.GetAllPaymentOrderUnpaidByModuleIdByLearner(moduleId,user.Id);
    
    }
    [HttpPost("GetAllPaymentOrderByModuleIdByLearnerId")]
    public async Task<ActionResult<List<LearningModulePaymentOrder>>> GetAllPaidPaymentOrderByModuleId(int moduleId)
    {
        var user = await _contextService.GetAppUserAndThrow();
        return await _paymentService.GetAllPaymentOrderByModuleIdByLearner(moduleId, user.Id);

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
