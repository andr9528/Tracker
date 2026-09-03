using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Dining.Persistence.Configuration;

public class DinnerTemplateConfiguration : EntityConfiguration<DinnerTemplate>
{
    /// <inheritdoc />
    public DinnerTemplateConfiguration(DatabaseType databaseType) : base(databaseType)
    {
    }

    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<DinnerTemplate> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
