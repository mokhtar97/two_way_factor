using Authenticaion.Domain.Entities;
using Authentication.Application.Common.Pagination;
using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(Object id);
        IEnumerable<TEntity> GetAll();
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        Task<TEntity> GetByIdAsync(Object id);
        Task<IEnumerable<TEntity>> GetAsync(
                                            Expression<Func<TEntity, bool>> predicate,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                            List<Expression<Func<TEntity, object>>> includes = null,
                                            bool disableTracking = false
                                           );
        Task<IEnumerable<TEntity>> GetAsync(
                                             Expression<Func<TEntity, bool>> predicate,
                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                             List<Expression<Func<TEntity, object>>> includes = null,
                                             bool disableTracking = false,
                                             string includeString = "");
        Task<PagedResult<TEntity>> GetAsync(int page, int pageSize,
                                                         Expression<Func<TEntity, bool>> predicate,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                         List<Expression<Func<TEntity, object>>> includes = null,
                                                         bool disableTracking = false,
                                                         string includeString = "");
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> InsertAsync(TEntity entity);
        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> Table { get; }
        IEnumerable<TEntity> FromSqlRaw(string storedProcedureName, params object[] parameters);
        Task<IEnumerable<TEntity>> FromSqlRawAsync(string storedProcedureName, params object[] parameters);
    }
}