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
        var user = await _contextService.GetAppUserAndThrow();

        return Ok(await _scheduleService.UpdateWeeklyLearningSession(id, user));
    }

    /// <summary>
    /// Add Weekly Schedule
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("AddWeeklySchedule")]
    public async Task<ActionResult<LearningModule>> AddWeeklySchedule(AddWeeklyScheduleDto dto)
    {
        var user = await _contextService.GetAppUserAndThrow();

        return Ok(await _scheduleService.AddWeeklySchedule(dto, user));
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
    /// Update Learning Session
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("UpdateLearningSession")]
    public async Task<ActionResult<LearningSession>> UpdateLearningSession(CreateCustomLearningSessionDto dto)
    {
        var tutor = await _contextService.GetAppUserAndThrow();
        return Ok(await _scheduleService.UpdateLearningSession(dto, tutor));
    }

    /// <summary>
    /// Delete Learning Session by ID
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("DeleteLearningSessionById")]
    public async Task<ActionResult<LearningSession>> DeleteLearningSessionById(int id)
    {
        return Ok(await _scheduleService.DeleteLearningSessionById(id));
    }
    /// <summary>
    /// Get Schedule For Tutor
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

    [Authorize(Roles = CustomRoles.Learner)]
    [HttpGet("ParticipateLearningSession")]
    public async Task<ActionResult<string>> ParticipateLearningSession(int learningSessionId)
    {
        var learner = await _contextService.GetAppUserAndThrow();
        return Ok(await _scheduleService.ParticipateLearningSession(learner.Id, learningSessionId));
    }

}
