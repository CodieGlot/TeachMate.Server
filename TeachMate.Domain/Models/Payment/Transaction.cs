using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public string BankCode { get; set; } = "";
        public string CardType { get; set; } = "";
        public string OrderInfo { get; set; } = "";
        public DateTime? PayDate { get; set; }
        public string ResponseCode { get; set; } = "";
        public string TmnCode { get; set; } = "";
        public string TransactionNo { get; set; } = "";
        public string TransactionStatus { get; set; } = "";
        public string TxnRef { get; set; } = "";
        public string SecureHash { get; set; } = "";
        public PaymentProviderType PaymentGateway { get; set; }

        public int LearningModulePaymentOrderId {  get; set; } 

        public LearningModulePaymentOrder LearningModulePaymentOrder { get; set; } = new LearningModulePaymentOrder();
    }
}
