using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class CreateTransactionDto
    {
        public double Amount { get; set; }
        
        public PaymentProviderType PaymentGateway { get; set; }

        public int LearningModulePaymentOrderId { get; set; }
    }
}
