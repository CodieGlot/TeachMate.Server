using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;
using TeachMate.Domain.Models.Payment;
using TeachMate.Services;

namespace TeachMate.Api;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAdminService _adminService;
    private readonly IPaymentService _paymentService;

    public AdminController(IUserService userService, IAdminService adminService, IPaymentService paymentService)
    {
        _userService = userService;
        _adminService = adminService;
        _paymentService = paymentService;
    }

    /// <summary>
    /// Get all user
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("GetAllUser")]
    public async Task<ActionResult<List<AppUser>>> GetAllUser()
    {
        return Ok(await _adminService.GetAllUser());
    }

    /// <summary>
    /// Search User
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPost("SearchUser")]
    public async Task<ActionResult<List<AppUser>>> SearchUser(SearchUserDto dto)
    {
        return Ok(await _adminService.SearchUser(dto));
    }

    /// <summary>
    /// Update status
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPut("UpdateStatus")]
    public async Task<ActionResult<AppUser>> UpdateStatus(DisableDto dto)
    {
        return Ok(await _adminService.UpdateStatus(dto));
    }

    /// <summary>
    /// Get report by ID
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("GetReportByID")]
    public async Task<ActionResult<Report>> GetReportByID(int id)
    {
        return Ok(await _adminService.GetReportByID(id));
    }

    /// <summary>
    /// Get all report system
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("GetAllReportSystem")]
    public async Task<ActionResult<List<Report>>> GetAllReportSystem()
    {
        return Ok(await _adminService.GetAllReportSystem());
    }

    /// <summary>
    /// Get all report user
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("GetAllReportUser")]
    public async Task<ActionResult<List<Report>>> GetAllReportUser()
    {
        return Ok(await _adminService.GetAllReportUser());
    }

    /// <summary>
    /// Search Report System
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPost("SearchReportSystem")]
    public async Task<ActionResult<List<Report>>> SearchReportSystem(SearchReportSystemDto dto)
    {
        return Ok(await _adminService.SearchReportSystem(dto));
    }

    /// <summary>
    /// Search Report User
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPost("SearchReportUser")]
    public async Task<ActionResult<List<Report>>> SearchReportUser(SearchReportUserDto dto)
    {
        return Ok(await _adminService.SearchReportUser(dto));
    }

    /// <summary>
    /// Update status report
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPut("UpdateStatusReport")]
    public async Task<ActionResult<Report>> UpdateStatusReport(UpdateStatusDto dto)
    {
        return Ok(await _adminService.UpdateStatusReport(dto));
    }

    /// <summary>
    /// Get payment by ID
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("GetPaymentByID")]
    public async Task<ActionResult<Report>> GetPaymentByID(int id)
    {
        return Ok(await _adminService.GetPaymentByID(id));
    }

    /// <summary>
    /// Get all payment order
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("GetAllPaymentOrder")]
    public async Task<ActionResult<List<LearningModulePaymentOrder>>> GetAllPaymentOrder()
    {
        return Ok(await _adminService.GetAllPaymentOrder());
    }

    /// <summary>
    /// Search Payment Order
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPost("SearchPaymentOrder")]
    public async Task<ActionResult<List<LearningModulePaymentOrder>>> SearchPaymentOrder(SearchPaymentOrderDto dto)
    {
        return Ok(await _adminService.SearchPaymentOrder(dto));
    }

    /// <summary>
    /// Update has claimed
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpPut("UpdateHasClaimed")]
    public async Task<ActionResult<LearningModulePaymentOrder>> UpdateHasClaimed(HasClaimedDto dto)
    {
        return Ok(await _adminService.UpdateHasClaimed(dto));
    }

    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("GetAccountInformationByTutorId")]
    public async Task<ActionResult<AccountInformation>> GetAccountInformationByTutorId(Guid id)
    {
        return Ok(await _paymentService.GetAccountInformationByTutorId(id));
    }

    /// <summary>
    /// Count tutor
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("CountTutor")]
    public async Task<ActionResult<int>> CountTutor()
    {
        return Ok(await _adminService.CountTutor());
    }

    /// <summary>
    /// Count learner
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("CountLearner")]
    public async Task<ActionResult<int>> CountLearner()
    {
        return Ok(await _adminService.CountLearner());
    }

    /// <summary>
    /// Count class
    /// </summary>
    [Authorize(Roles = CustomRoles.Admin)]
    [HttpGet("CountClass")]
    public async Task<ActionResult<int>> CountClass()
    {
        return Ok(await _adminService.CountClass());
    }
}
