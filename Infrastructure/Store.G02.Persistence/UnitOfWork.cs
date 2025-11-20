using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {

        private Dictionary<string, object> _repositories = new Dictionary<string, object>();
        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {
           var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TKey, TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return (IGenericRepository < TKey, TEntity >) _repositories[type] ;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
