using MobLib.Payment.PayU.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MobLib.Payment.PayU.Data.Mapping
{
    public class ResponseMap : EntityTypeConfiguration<Response>
    {
        public ResponseMap()
        {
            this.ToTable("PayU.Response");
            this.HasKey(x => x.Id);
            this.Property(x => x.SubscriptionId).IsOptional();
            this.Property(x => x.SubscritionFound).IsRequired();
            this.Property(x => x.ResponseCode).IsRequired();
            this.Property(x => x.TransactionDate).IsOptional();
            this.Property(x => x.Description).IsOptional();
            this.Property(x => x.ResponseMessage).IsOptional();
            this.Property(x => x.TransactionId).IsOptional();
            this.Property(x => x.DateNextPayment).IsOptional();
            this.Property(x => x.Sign).IsOptional();
            this.Property(x => x.Reference).IsOptional();
            this.Property(x => x.ReferenceSale).IsOptional();
            this.Property(x => x.AuthorizationCode).IsOptional();
            this.Property(x => x.ReferenceRecurringPayment).IsOptional();

            this.HasOptional(x => x.Subscription)
                .WithMany(x => x.Responses)
                .HasForeignKey(x => x.SubscriptionId);
        }
    }
}
