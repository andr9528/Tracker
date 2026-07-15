using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Configuration
{
    public class DishConfiguration : EntityConfiguration<Dish>
    {
        /// <inheritdoc />
        public DishConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Dish> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => (ICollection<DishIngredient>) x.DishIngredients)
                .WithOne(x => (Dish) x.Dish)
                .HasForeignKey(x => x.DishId);
        }
    }
}
