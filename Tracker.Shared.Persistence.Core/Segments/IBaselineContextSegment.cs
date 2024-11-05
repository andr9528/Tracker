using Microsoft.EntityFrameworkCore;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Models.User;

namespace Tracker.Shared.Persistence.Core.Segments
{
    public interface IBaselineContextSegment : IContextSegment
    {
        DbSet<CoreUser> CoreUsers { get; set; }
    }
}