using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Domain;

namespace TeachMate.Services
{
    public interface IEmailOtp
    {
        Task<ResponseDto> SendEmailOtp(EmailReciveDto dto);
    }
}
