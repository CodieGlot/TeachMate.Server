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
    private readonly IHttpContextService _contextService;
    public ScheduleController(IScheduleService scheduleService, IHttpContextService contextService)
    {
        _scheduleService = scheduleService;
        _contextService = contextService;
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

    /// <summary>
    /// Add Custom Schedule
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("CreateCustomLearning")]
    public async Task<ActionResult<LearningSession>> CreateCustomLearning(CreateCustomLearningSessionDto dto)
    {
        var tutor = await _contextService.GetAppUserAndThrow();
        return Ok(await _scheduleService.CreateCustomLearningSession(dto, tutor));
    }

    /// <summary>
    /// Get Schedule by Id
    /// </summary>
    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpGet("GetScheduleById")]
    public async Task<ActionResult<List<LearningSession>>> GetScheduleById(int id)
    {
        return Ok(await _scheduleService.GetScheduleById(id));
    }
    /// <summary>
    /// Get Schedule by Id
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("GetScheduleByTutor")]
    public async Task<ActionResult<List<LearningSession>>> GetScheduleByTutor()
    {
        var tutor = await _contextService.GetAppUserAndThrow();
        return Ok(await _scheduleService.GetScheduleByTutor(tutor));
    }

    [Authorize(Roles = CustomRoles.Learner)]
    [HttpPost("GetScheduleByLearner")]
    public async Task<ActionResult<List<LearningSession>>> GetScheduleByLearner()
    {
        var learner = await _contextService.GetAppUserAndThrow();
        return Ok(await _scheduleService.GetScheduleByLearner(learner));
    }

    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpGet("GetLearningSessionById")]
    public async Task<ActionResult<LearningSession>> GetLearningSessionById(int id)
    {
        return Ok(await _scheduleService.GetLearningSessionById(id));
    }

    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("CreateFreeLearningSession")]
    public async Task<ActionResult<LearningSession>> CreateFreeLearningSession(CreateCustomLearningSessionDto dto)
    {
        var tutor = await _contextService.GetAppUserAndThrow();

        return Ok(await _scheduleService.CreateFreeLearningSession(dto, tutor));
    }

}
