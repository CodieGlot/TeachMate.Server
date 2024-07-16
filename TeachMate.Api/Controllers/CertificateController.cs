using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Domain.DTOs.SearchDto;
using TeachMate.Domain.Models.Certificate;
using TeachMate.Services;

namespace TeachMate.Api;

[Route("api/[controller]")]
[ApiController]
public class CertificateController : ControllerBase
{
    private readonly ICertificateService _certificateService;
    private readonly IHttpContextService _contextService;

    public CertificateController(IHttpContextService contextService, ICertificateService certificateService)
    {
        _contextService = contextService;
        _certificateService = certificateService;
    }

    /// <summary>
    /// Get all user
    /// </summary>
    [Authorize(Roles = CustomRoles.Tutor)]
    [HttpPost("UploadCertificate")]
    public async Task<ActionResult<Certificate>> UploadCertificate(UploadCertificateDto dto)
    {
        var user = await _contextService.GetAppUserAndThrow();
        return Ok(await _certificateService.UploadCertificate(dto, user.Id));
    }

    [Authorize(Roles = CustomRoles.GeneralUser)]
    [HttpPost("GetAllCertificateByTutorId")]
    public async Task<ActionResult<Certificate>> GetAllCertificateByTutorId(Guid tutorId)
    {
        return Ok(await _certificateService.GetAllCertificatesByTutorId(tutorId));
    }

}
