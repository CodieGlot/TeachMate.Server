using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain.Models.Payment
{
    public class AccountInformation
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string TaxCode { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get;set; }
        public Guid TutorId { get; set; }
        public virtual Tutor Tutor  { get; set; }
    }
}
