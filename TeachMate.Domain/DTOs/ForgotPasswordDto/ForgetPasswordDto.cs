using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class ForgetPasswordDto
    {
       public String Email { get; set; }
       public String NewPassword { get; set; } 
       public String ConfirmPassword { get; set; }
    }
}
