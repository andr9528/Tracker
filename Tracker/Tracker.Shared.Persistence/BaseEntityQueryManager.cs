using Tracker.Shared.Abstraction.Interfaces.Persistence;

namespace Tracker.Shared.Persistence
{
    public abstract class BaseEntityQueryManager<TContext, TEntity, TSearchable> : IEntityQueryManager<TEntity, TSearchable>
        where TContext : BaseDatabaseContext where TEntity : class, IEntity where TSearchable : class, ISearchable
    {
        protected readonly TContext context;

        protected BaseEntityQueryManager(TContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<TEntity> AddEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> AddEntities(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetEntity(TSearchable searchable)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetEntities(TSearchable searchable)
        {
            throw new NotImplementedException();
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