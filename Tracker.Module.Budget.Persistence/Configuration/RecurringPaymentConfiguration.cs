using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Configuration
{
    public class RecurringPaymentConfiguration : EntityConfiguration<RecurringPayment>
    {
        /// <inheritdoc />
        public RecurringPaymentConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<RecurringPayment> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => (ICollection<Payment>) x.Payments).WithOne(x => (RecurringPayment) x.RecurringPayment)
                .HasForeignKey(x => x.RecurringPaymentId);

            builder.HasOne(x => (PaymentTemplate) x.PaymentTemplate).WithOne(x => (RecurringPayment) x.RecurringPayment)
                .HasForeignKey<PaymentTemplate>(x => x.RecurringPaymentId);
        }
    }
}