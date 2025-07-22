using Microsoft.EntityFrameworkCore;
using Travellin.Core.Dtos;
using Travellin.Core.Entities;
using Travellin.Core.Interfaces;
using Travellin.Travellin.Core.Shared;
using Travellin.Infrastructure.Data;
using System.Linq.Expressions;
using Microsoft.Graph.Models;

namespace Travellin.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TKey> : BaseRepository, IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public GenericRepository(TravellinDbContext dbContext) : base(dbContext)
        { }

        public virtual void Create(TEntity entity)
        {
            _dbContext.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public void Delete(TEntity entity)
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

        public virtual TEntity GetById(TKey id)
        {
            return _dbContext.Set<TEntity>().Find(id) ?? throw new Exception("NUll");
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




        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
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

        public async Task DeleteAsync(string propertyId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // First, delete all related PropertyAvailabilities
                var propertyAvailabilities = await _dbContext.PropertyAvailabilities
                    .Where(pa => pa.PropertyId == propertyId)
                    .ToListAsync();

                if (propertyAvailabilities.Any())
                {
                    _dbContext.PropertyAvailabilities.RemoveRange(propertyAvailabilities);
                }

                // Then delete any other related records (bookings, reviews, etc.)
                var bookings = await _dbContext.Bookings
                    .Where(b => b.PropertyId == propertyId)
                    .ToListAsync();

                if (bookings.Any())
                {
                    _dbContext.Bookings.RemoveRange(bookings);
                }

                // Finally, delete the property
                var property = await _dbContext.Properties.FindAsync(propertyId);
                if (property != null)
                {
                    _dbContext.Properties.Remove(property);
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

    }
}
