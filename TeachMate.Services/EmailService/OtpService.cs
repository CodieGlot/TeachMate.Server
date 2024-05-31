using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachMate.Services;

namespace TeachMate.Services
{
    public class OtpService : IOtpService
    {
        private string _otp;
        private string _email;

        public void SetOtp(string otp)
        {
            _otp = otp;
        }

        public string GetOtp()
        {
            return _otp;
        }

        public string GetEmail()
        {
            return _email;
        }
        public void SetEmail(string email)
        {
            _email = email;
        }

    }
}
