using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Module.Budget.Model.Entity;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Configuration
{
    public class CurrencyRateConfiguration : EntityConfiguration<CurrencyRate>
    {
        /// <inheritdoc />
        public CurrencyRateConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            base.Configure(builder);
        }
    }
}
