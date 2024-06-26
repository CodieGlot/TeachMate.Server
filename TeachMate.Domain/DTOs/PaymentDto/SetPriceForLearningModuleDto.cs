using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class SetPriceForLearningModuleDto
    {
        public int LearningModuleId { get; set; }
        public double Price { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
