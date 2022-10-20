using Authentication.Application.Common.Interfaces;
using Authentication.Application.Common.Pagination;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Presistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private DbSet<TEntity> entities;
        private DbContext _dbContext;
        public IQueryable<TEntity> Table => this.entities;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<TEntity>();
        }
        public void Delete(TEntity entity)
        {
            this.entities.Remove(entity);
        }
        public void Delete(IEnumerable<TEntity> entities)
        {
            this.entities.RemoveRange(entities);
        }
        public TEntity GetById(object id)
        {
            return this.entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.entities;
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await this.entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.entities.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAsync(
                                                 Expression<Func<TEntity, bool>> predicate,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                 List<Expression<Func<TEntity, object>>> includes =null,
                                                 bool disableTracking =false
                                                )
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

           

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }
        public async Task<PagedResult<TEntity>> GetAsync(int page, int pageSize,
                                                         Expression<Func<TEntity, bool>> predicate,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                         List<Expression<Func<TEntity, object>>> includes = null,
                                                         bool disableTracking =false,
                                                         string includeString = "")
        {
                IQueryable<TEntity> query = _dbContext.Set<TEntity>();
                if (disableTracking) query = query.AsNoTracking();

                if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

                if (predicate != null) query = query.Where(predicate);

                if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

                if (orderBy != null)
                    query = orderBy(query);

              return await query.GetPaged<TEntity>(page, pageSize);
        }
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await this.entities.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities)
        {
            await this.entities.AddRangeAsync(entities);
            return entities;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.FromResult(this.entities.Update(entity));
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(this.entities.Remove(entity));
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
                await Task.FromResult(this.entities.Remove(item));
        }

        public IEnumerable<TEntity> FromSqlRaw(string storedProcedureName, params object[] parametars)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(storedProcedureName, parametars).ToList();
        }
        public async Task<IEnumerable<TEntity>> FromSqlRawAsync(string storedProcedureName, params object[] parametars)
        {
            return await _dbContext.Set<TEntity>().FromSqlRaw(storedProcedureName, parametars).ToListAsync();
        }

        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, List<Expression<Func<TEntity, object>>> includes = null, bool disableTracking = false, string includeString = "")
        {
            throw new NotImplementedException();
        }
    }
}
