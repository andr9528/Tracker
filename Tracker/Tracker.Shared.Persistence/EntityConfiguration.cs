using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence.Converters;

namespace Tracker.Shared.Persistence
{
    public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        private readonly DatabaseType databaseType;

        protected EntityConfiguration(DatabaseType databaseType)
        {
            this.databaseType = databaseType;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            Type type = typeof(TEntity);
            var idName = $"{type.Name}Id";

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(idName);

            switch (databaseType)
            {
                case DatabaseType.SQlite:
                    builder.Property(x => x.Version).IsRowVersion().HasConversion(new SqliteTimestampConverter())
                        .HasColumnType("BLOB").HasDefaultValueSql("CURRENT_TIMESTAMP");
                    break;
                case DatabaseType.MS_SQL:
                    builder.Property(x => x.Version).IsRowVersion();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<object>(x =>
            {

            });
        }
    }
}