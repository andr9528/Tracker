using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Configuration
{
    public class DishIngredientConfiguration : EntityConfiguration<DishIngredient>
    {
        /// <inheritdoc />
        public DishIngredientConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<DishIngredient> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => (Ingredient) x.Ingredient)
                .WithMany(x => (ICollection<DishIngredient>) x.DishIngredients)
                .HasForeignKey(x => x.IngredientId);
        }
    }
}
