using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Configuration
{
    public class IngredientConfiguration : EntityConfiguration<Ingredient>
    {
        /// <inheritdoc />
        public IngredientConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            base.Configure(builder);
        }
    }
}
