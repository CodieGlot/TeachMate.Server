using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.ScheduleDto;
using TeachMate.Services;

namespace TeachMate.Api;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;
    private readonly IAdminService _adminService;   

    public ScheduleController(IScheduleService scheduleService, IAdminService adminService)
    {
        _scheduleService = scheduleService;
        _adminService = adminService;
    }

    /// <summary>
    /// Auto update weekly sessions
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("UpdateWeeklySessions")]
    public async Task<ActionResult<LearningModule>> UpdateWeeklySlots(int id)
    {
        return Ok(await _scheduleService.UpdateWeeklyLearningSession(id));
    }
    /// <summary>
    /// Add Weekly Schedule
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("AddWeeklySchedule")]
    public async Task<ActionResult<LearningModule>> AddWeeklySchedule(AddWeeklyScheduleDto dto)
    {
        return Ok(await _scheduleService.AddWeeklySchedule(dto));
    }



}
