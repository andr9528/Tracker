using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Module.Budget.Model.Entity;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Configuration
{
    public class PaymentTemplateConfiguration : EntityConfiguration<PaymentTemplate>
    {
        /// <inheritdoc />
        public PaymentTemplateConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<PaymentTemplate> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => (PaymentType) x.PaymentType).WithOne();
        }
    }
}
