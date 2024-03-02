using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Configuration
{
    public class PaymentTypeConfiguration : EntityConfiguration<PaymentType>
    {
        /// <inheritdoc />
        public PaymentTypeConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            base.Configure(builder);
        }
    }
}