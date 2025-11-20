using Store.G02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface ISpecifications<TKey,TEntity> where TEntity : BaseEntity<TKey>
    {
         List<Expression<Func<TEntity, object>>> Includes { get; set; }
         Expression<Func<TEntity, bool>>? Criteria { get; set; }

         Expression<Func<TEntity, object>>? OrderyBy { get; set; }   
         Expression<Func<TEntity, object>>? OrderyByDescending { get; set; } 
        
        int Skip { get; set; }

        int Take { get; set; }

        bool IsPagination { get; set; }
    }
}
