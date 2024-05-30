using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Services
{
    public interface IOtpService
    {
         void SetOtp(string otp);
        public string GetOtp();
        public string GetEmail();

        public void SetEmail(string email);
        
    }
}
