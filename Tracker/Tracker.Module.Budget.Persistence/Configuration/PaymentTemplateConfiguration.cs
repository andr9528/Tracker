using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Persistence;

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