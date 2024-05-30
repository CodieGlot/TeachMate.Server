using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class UserOTP
    {
        [Key]
        public int id { get; set; }
        public string OTP { get; set; }
        public string gmail { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public DateTime ExpireAt { get; set; } = DateTime.Now.AddMinutes(10);


        
    }
}
