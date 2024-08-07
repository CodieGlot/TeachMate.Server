﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    //private readonly IUserService _userService;
    private readonly IReportService _reportService;
    private readonly IHttpContextService _contextService;

    public ReportController(IReportService reportService, IHttpContextService contextService)
    {
        _reportService = reportService;
        _contextService = contextService;
    }

    /// <summary>
    /// Sent report system
    /// </summary>
    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpPost("SentReportSystem")]
    public async Task<ActionResult<AppUser>> SentReportSystem(ReportSystemDto dto)
    {
        var user = await _contextService.GetAppUserAndThrow();
        return Ok(await _reportService.SentReportSystem(dto, user));
    }

    /// <summary>
    /// Sent report user
    /// </summary>
    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpPost("SentReportUser")]
    public async Task<ActionResult<AppUser>> SentUserReport(ReportUserDto dto)
    {
        var user = await _contextService.GetAppUserAndThrow();
        return Ok(await _reportService.SentReportUser(dto, user));
    }
}
