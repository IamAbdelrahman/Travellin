using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;
using System.Linq.Expressions;

namespace Travellin.Infrastructure.Repositories
{
    class GenericRepository<TEntity, TKey> : BaseRepository, IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public GenericRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public virtual void Create(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null) return;

            _dbContext.Remove(entity);
        }

        public virtual Task<PaginatedResult<TEntity>> GetAllAsync(GetAllQueryDto queryDto, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return GetPaginatedResultAsync(queryDto, orderBy);
        }

        public virtual Task<PaginatedResult<TEntity>> GetAllAsync(GetAllQueryDto queryDto, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes)
        {
            return GetPaginatedResultAsync(queryDto, orderBy, includes);
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (includes is not null && includes.Length > 0)
            {
                query = ApplyIncludesToQuery(query, includes);
            }

            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        private async Task<PaginatedResult<TEntity>> GetPaginatedResultAsync(GetAllQueryDto queryDto, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[]? includes)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            // Calc pagination meta data
            var metaData = new PaginationMetaData
            {
                Page = queryDto.Page,
                PageSize = queryDto.PageSize,
                Total = await query.CountAsync()
            };

            // Apply includes
            if (includes is not null && includes.Length > 0)
            {
                query = ApplyIncludesToQuery(query, includes);
            }

            // Apply ordering
            query = orderBy(query);

            // Evaluate Query
            var items = await query.AsNoTracking()
                    .Skip(queryDto.CalcSkippedItems())
                    .Take(queryDto.PageSize)
                    .ToListAsync();

            return new PaginatedResult<TEntity>
            {
                Items = items,
                MetaData = metaData
            };
        }

        private IQueryable<TEntity> ApplyIncludesToQuery(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var expr in includes)
            {
                query = query.Include(expr);
            }

            return query;
        }
    }
}
