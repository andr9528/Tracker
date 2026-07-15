using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Configuration
{
    public class DinnerConfiguration : EntityConfiguration<Dinner>
    {
        /// <inheritdoc />
        public DinnerConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Dinner> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => (Dish) x.Dish)
                .WithMany(x => (ICollection<Dinner>) x.Dinners)
                .HasForeignKey(x => x.DishId);
        }
    }
}
