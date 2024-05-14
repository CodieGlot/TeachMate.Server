using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api;
[Route("api/[controller]")]
[ApiController]
public class LearningModuleController : ControllerBase
{
    private readonly IHttpContextService _contextService;
    private readonly ILearningModuleService _learningModuleService;

    public LearningModuleController(ILearningModuleService learningModuleService, IHttpContextService contextService)
    {
        _learningModuleService = learningModuleService;
        _contextService = contextService;
    }

    /// <summary>
    /// Get LearningModule by Id
    /// </summary>
    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LearningModule>> GetLearningModuleById(int id)
    {
        return Ok(await _learningModuleService.GetLearningModuleById(id));
    }

    /// <summary>
    /// Create LearningModule
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("Create")]
    public async Task<ActionResult<LearningModule>> CreateLearningModule(CreateLearningModuleDto dto)
    {
        var tutor = await _contextService.GetAppUserAndThrow();
        return Ok(await _learningModuleService.CreateLearningModule(tutor, dto));
    }
}
