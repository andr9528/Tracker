using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Persistence;

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