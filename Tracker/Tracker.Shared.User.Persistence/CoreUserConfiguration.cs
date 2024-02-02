using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Models.Modules.Budget;
using Tracker.Shared.Models.Modules.Budget.Entity;
using Tracker.Shared.Models.User;
using Tracker.Shared.Persistence;

namespace Tracker.Shared.User.Persistence
{
    public class CoreUserConfiguration : EntityConfiguration<CoreUser>
    {
        /// <inheritdoc />
        public CoreUserConfiguration(DatabaseType databaseType) : base(databaseType)
        {
        }

        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<CoreUser> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => (ICollection<RecurringPayment>) x.RecurringPayments)
                .WithOne(x => (CoreUser) x.CoreUser).HasForeignKey(x => x.CoreUserId);

            builder.HasMany(x => (ICollection<Payment>) x.Payments).WithOne(x => (CoreUser) x.CoreUser)
                .HasForeignKey(x => x.CoreUserId);
        }
    }
}