using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Enums.Persistence;

namespace Tracker.Shared.Persistence
{
    public interface IContextSegment
    {
        ContextSegmentType SegmentType { get; }

        void OnModelCreating(ModelBuilder modelBuilder, DatabaseType databaseType);
    }
}