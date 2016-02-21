using MobLib.Core.Domain.Entities;
using System.Collections.Generic;

namespace MobLib.Payment.PayU.Domain.Entities
{
    public class Plan : MobEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Virtual methods are ok for entities classes")]
        public Plan()
        {
            this.AdditionalValues = new HashSet<AdditionalValue>();
        }

        public string PlanPayUId { get; set; }
        public string AccountId { get; set; }
        public string PlanCode { get; set; }
        public string Description { get; set; }
        public int IntervalId { get; set; }
        public int IntervalCount { get; set; }
        public int MaxPaymentsAllowed { get; set; }
        public int? MaxPaymentAttempts { get; set; }
        public int? PaymentAttemptsDelay { get; set; }
        public int? MaxPendingPayments { get; set; }
        public int? TrialDays { get; set; }
        public virtual ICollection<AdditionalValue> AdditionalValues { get; set; }
        public virtual PlanInterval PlanInterval { get; set; }
    }
}
