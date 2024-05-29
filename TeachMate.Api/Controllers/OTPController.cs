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
        private readonly DataContext _context;

        public OTPController(IEmailOtp IEmailOTP, IHttpContextService contextService, DataContext context)
        {
            _IEmailOTP = IEmailOTP;
            _contextService = contextService;
            _context = context;
        }

        [HttpPost("SendOTP")]
        public  async Task<ActionResult> SendOtp(EmailReciveDto dto) {
            
                await _IEmailOTP.SendEmailOtp(dto);
            return Ok("Email send success");     
            
        }
    }
}
