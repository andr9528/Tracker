using Tracker.Shared.Abstraction.Enums.Persistence;
using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Module.Budget.Persistence.Query
{
    public abstract class
        BaseBudgetQueryManager<TEntity, TSearchable, TDto> : BaseEntityQueryManager<TrackerDatabaseContext, TEntity,
            TSearchable, TDto> where TEntity : class, IEntity
        where TSearchable : class, ISearchable
        where TDto : class, IDto
    {
        /// <inheritdoc />
        protected BaseBudgetQueryManager(TrackerDatabaseContext context) : base(context)
        {
        }

        protected BudgetDatabaseContext GetContextSegment()
        {
            return (context.GetContextSegment(ContextSegmentType.BUDGET) as BudgetDatabaseContext)!;
        }
    }
}