using System;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class ResponseModel
    {
        public int response_code_pol { get; set; }
        public DateTime? transaction_date { get; set; }
        public string description { get; set; }
        public string response_message_pol { get; set; }
        public string transaction_id { get; set; }
        public DateTime? date_next_payment { get; set; }
        public string sign { get; set; }
        public string reference_pol { get; set; }
        public string reference_sale { get; set; }
        public string authorization_code { get; set; }
        public string reference_recurring_payment { get; set; }
    }
}