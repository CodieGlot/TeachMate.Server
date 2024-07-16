using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class AddAccountInformationDto
    {
        public string FullName { get; set; }
        public string TaxCode { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
    }
}
