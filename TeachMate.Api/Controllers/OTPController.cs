using Microsoft.AspNetCore.Mvc;
using TeachMate.Domain;
using TeachMate.Services;

namespace TeachMate.Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class OTPController : ControllerBase { 
        public readonly IEmailOtp _IEmailOTP;
        private readonly IHttpContextService _contextService;

        public OTPController(IEmailOtp IEmailOTP, IHttpContextService contextService)
        {
            _IEmailOTP = IEmailOTP;
            _contextService = contextService;
        }

        [HttpPost("SendOTP")]
        public async Task<ActionResult> SendOtp(EmailReciveDto dto)
        {

           
            return Ok(await _IEmailOTP.SendEmailOtp(dto));

        }
    }
}
