using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
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
        }
    }
}