using Tracker.Shared.Models.User;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Shared.User.Persistence.Query
{
    public class CoreUserQueryManager : BaseEntityQueryManager<TrackerDatabaseContext, CoreUser, SearchableCoreUser,
        CoreUserDto>
    {
        /// <inheritdoc />
        public CoreUserQueryManager(TrackerDatabaseContext context) : base(context)
        {
        }

        /// <inheritdoc />
        protected override CoreUser BuildEntity(CoreUserDto dto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override IQueryable<CoreUser> GetBaseQuery()
        {
            return context.CoreUsers.AsQueryable();
        }

        /// <inheritdoc />
        protected override IQueryable<CoreUser> AddQueryArguments(
            SearchableCoreUser searchable, IQueryable<CoreUser> query)
        {
            throw new NotImplementedException();
        }
    }
}