using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence.Core.Converters;

namespace Tracker.Shared.Persistence.Core;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    private readonly DatabaseType type;

    protected EntityConfiguration(DatabaseType type)
    {
        this.type = type;
    }

    /// <inheritdoc />
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        Type type = typeof(TEntity);
        var idName = $"{type.Name}Id";

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName(idName);

        switch (this.type)
        {
            case DatabaseType.SQLITE:
                builder.Property(x => x.Version).IsRowVersion().HasConversion(new SqliteTimestampConverter())
                    .HasColumnType("BLOB").HasDefaultValueSql("CURRENT_TIMESTAMP");

                break;
            case DatabaseType.POSTGRESQL or DatabaseType.MS_SQL:
                builder.Property(x => x.Version).IsRowVersion();
                break;
        }
    }
}
