using Authentication.Application.Common.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Application.Common.Interfaces;
using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthenticationContext _context;
        public Dictionary<Type, object> Repositories;
      

        public UnitOfWork()
        {

        }
        public UnitOfWork(AuthenticationContext skeletonContext)
        {
            _context = skeletonContext;
            this.Repositories = new Dictionary<Type, object>();
        }

      
        public void Dispose()
        {
            this._context.Dispose();
        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (!Repositories.ContainsKey(typeof(TEntity)))
            {
                var repositoryObject = Activator.CreateInstance(typeof(Repository<TEntity>), this._context);
                this.Repositories.Add(typeof(TEntity), repositoryObject);
            }
            return this.Repositories[typeof(TEntity)] as IRepository<TEntity>;
        }
        public async Task<int> SaveChanges()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
