﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Persistence.Core
{
    public abstract class BaseDatabaseContext : DbContext
    {
        private readonly DbContextOptions options;
        protected readonly ICollection<IContextSegment> segments;

        protected BaseDatabaseContext([NotNull] DbContextOptions options) : base(options)
        {
            this.options = options;
            segments = new List<IContextSegment>();
        }

        protected void AddContextSegment(IContextSegment segment)
        {
            segments.Add(segment);
        }

        public IContextSegment GetContextSegment(ContextSegmentType segmentType)
        {
            return segments.FirstOrDefault(x => x.SegmentType == segmentType) ??
                   throw new NullReferenceException(
                       $"Expected a context segment for '{segmentType}' to exist, but null was returned");
        }

        public override int SaveChanges()
        {
            UpdateDatetimes();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateDatetimes();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateDatetimes();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateDatetimes();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateDatetimes()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IEntity && e.State is EntityState.Added or EntityState.Modified);

            foreach (EntityEntry entityEntry in entries)
            {
                ((IEntity) entityEntry.Entity).UpdatedDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                    ((IEntity) entityEntry.Entity).CreatedDateTime = DateTime.Now;
            }
        }
    }
}