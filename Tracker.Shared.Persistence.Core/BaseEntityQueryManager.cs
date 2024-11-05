using Tracker.Shared.Abstraction.Interfaces.Persistence;
using Tracker.Shared.Persistence.Core;

namespace Tracker.Shared.Persistence
{
    public abstract class
        BaseEntityQueryManager<TEntity, TSearchable, TSegment> : IEntityQueryManager<TEntity, TSearchable>
        where TEntity : class, IEntity where TSearchable : class, ISearchable where TSegment : IContextSegment
    {
        protected readonly BaseDatabaseContext context;
        protected readonly TSegment segment;

        protected BaseEntityQueryManager(BaseDatabaseContext context, TSegment segment)
        {
            this.context = context;
            this.segment = segment;
        }

        /// <inheritdoc />
        public async Task<TEntity> AddEntity(TEntity entity)
        {
            return context.Add(entity).Entity; // <-- Incorrect retrieval of the supposed result
        }


        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> AddEntities(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetEntity(TSearchable searchable)
        {
            return BuildQuery(searchable).ToList().First();
        }

        private IQueryable<TEntity> BuildQuery(TSearchable searchable)
        {
            IQueryable<TEntity> query = GetBaseQuery();

            if (searchable.Id != default)
                query = query.Where(x => x.Id == searchable.Id);

            query = AddQueryArguments(searchable, query);

            return query;
        }

        protected abstract IQueryable<TEntity> GetBaseQuery();
        protected abstract IQueryable<TEntity> AddQueryArguments(TSearchable searchable, IQueryable<TEntity> query);

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetEntities(TSearchable searchable)
        {
            return BuildQuery(searchable).ToList();
        }

        /// <inheritdoc />
        public async Task<TEntity> UpdateEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> UpdateEntities(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task DeleteEntity(TSearchable searchable)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task DeleteEntityById(int id)
        {
            throw new NotImplementedException();
        }
    }
}