using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities;
using Store.G02.Domain.Entities.Products;
using Store.G02.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repository
{
    public class GenericRepository<TKey, TEntity>(StoreDbContext _context) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>

    {


        public async Task AddAsync(TEntity entity)
        {
             await _context.AddAsync(entity);
        }

        public async Task<int> CountAsync(ISpecifications<TKey, TEntity> spec)
        {
         return await ApplySpecifications(spec).CountAsync();
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public  async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TKey , TEntity> spec,bool changeTracker = false)
        {
         return  await ApplySpecifications(spec).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
        {
            if (typeof(TEntity) == typeof(Product))
            { 
                return changeTracker ?
                    await _context.Products.Skip(5 * 19).Take(5).Include(P => P.Brand).Include(P => P.Type).OrderBy(P => P.Price).ToListAsync() as IEnumerable<TEntity>
                    : await _context.Products.Include(P => P.Brand).Include(P => P.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return changeTracker ?
                await _context.Set<TEntity>().ToListAsync()
                                 : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey key)
        {
            if (typeof(TEntity) == typeof(Product))
            {
               // return await _context.Products.Include(P=> P.Brand).Include(P => P.Type).FirstOrDefaultAsync(p => p.Id==key as int?) as TEntity;
                return await _context.Products.Include(P=> P.Brand).Include(P => P.Type).Where(p => p.Id == key as int?).FirstOrDefaultAsync() as TEntity;
            }
                return   await _context.Set<TEntity>().FindAsync(key);
        }

        public async Task<TEntity?> GetAsync(ISpecifications<TKey, TEntity> spec)
        {
          return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }


        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TKey,TEntity>spec)
        {
            return SpecificationEvaluator.GerQuery(_context.Set<TEntity>(), spec);
        }
    }
}
