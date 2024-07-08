using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;
using TeachMate.Services;

namespace TeachMate.Api;

[Route("api/[controller]")]
[ApiController]
public class LearningMaterialController : ControllerBase
{
    private readonly ILearningMaterialService _learningMaterialService;

    public LearningMaterialController(ILearningMaterialService learningMaterialService)
    {
        _learningMaterialService = learningMaterialService;
    }


    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("Chapter/Create")]
    public async Task<ActionResult<LearningChapter>> AddLearningChapter(AddLearningChapterDto dto)
    {
        return Ok(await _learningMaterialService.AddLearningChapter(dto));
    }

    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("Material/Upload")]
    public async Task<ActionResult<LearningMaterial>> UploadLearningMaterial(UploadLearningMaterialDto dto)
    {
        return Ok(await _learningMaterialService.UploadLearningMaterial(dto));
    }
    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpGet("Material/GetAll")]
    public async Task<ActionResult<List<LearningMaterial>>> GetMaterialByChapterId(int chapterId)
    {
        return Ok(await _learningMaterialService.GetAllLearningMaterialsByLearningChapterId(chapterId));
    }
    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpGet("Chapter/GetAll")]
    public async Task<ActionResult<List<LearningChapter>>> GetChapterByLearningModuleId(int moduleId)
    {
        return Ok(await _learningMaterialService.GetAllLearningChaptersByLearningModuleId(moduleId));
    }

}
