using Tracker.Shared.Models.User;
using Tracker.Shared.Persistence;

namespace Tracker.Shared.User.Persistence
{
    public class CoreUserQueryManager<TContext> : BaseEntityQueryManager<TContext, CoreUser, SearchableCoreUser> where TContext : BaseDatabaseContext
    {
        /// <inheritdoc />
        public CoreUserQueryManager(TContext context) : base(context)
        {
        }
    }
}