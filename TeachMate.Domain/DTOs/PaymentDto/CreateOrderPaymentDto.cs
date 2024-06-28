using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class CreateOrderPaymentDto
    {
        public Guid LearnerID { get; set; }
        public int LearningModuleId {get;set;}

    }
}
