using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GerQuery<TKey, TEntity>(IQueryable<TEntity> inputQuery, ISpecifications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
            {
                query = inputQuery.Where(spec.Criteria);
            }

            if(spec.OrderyBy is not null)
            {
                query = query.OrderBy(spec.OrderyBy);
            }else if(spec.OrderyByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderyByDescending);
            }

            if (spec.IsPagination) 
            
            { 
            query = query.Skip(spec.Skip).Take(spec.Take);

            }

            query = spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));

            return query;
        }
    }
}
