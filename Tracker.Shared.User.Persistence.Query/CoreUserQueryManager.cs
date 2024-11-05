using Tracker.Shared.Models.User;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;
using Tracker.Shared.Persistence.Core.Segments;

namespace Tracker.Shared.User.Persistence.Query
{
    public class CoreUserQueryManager : BaseEntityQueryManager<CoreUser, SearchableCoreUser, IBaselineContextSegment>
    {
        /// <inheritdoc />
        public CoreUserQueryManager(TrackerDatabaseContext context, IBaselineContextSegment baselineContextSegment) :
            base(context, baselineContextSegment)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<CoreUser> GetBaseQuery()
        {
            return segment.CoreUsers.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<CoreUser> AddQueryArguments(
            SearchableCoreUser searchable, IQueryable<CoreUser> query)
        {
            throw new NotImplementedException();
        }
    }
}