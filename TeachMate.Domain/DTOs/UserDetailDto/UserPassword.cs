using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class UserPassword
    {
        
        public String Old_Password { get; set; }
        public String New_Password { get; set; }
        public String Confirm_Password { get; set; }
    }
}
