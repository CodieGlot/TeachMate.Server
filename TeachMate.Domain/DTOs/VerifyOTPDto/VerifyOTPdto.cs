using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class VerifyOTPDto
    {
        public String Email { get; set; }
        public String OTP1 { get; set; }
        public String OTP2 { get; set; }

        public String OTP3 { get; set; }

        public String OTP4 { get; set; }
    }
}
