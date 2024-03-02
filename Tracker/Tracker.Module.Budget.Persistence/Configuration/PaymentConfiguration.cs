using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Configuration
{
    public class PaymentConfiguration : EntityConfiguration<Payment>
    {
        /// <inheritdoc />
        public PaymentConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => (PaymentType) x.PaymentType).WithOne();
        }
    }
}