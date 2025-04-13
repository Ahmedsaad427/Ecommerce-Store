using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        // Generate Query
        public static IQueryable<TEntity> GetQuery<TEntity,Tkey>(IQueryable<TEntity> InputQuery,ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        { 
        var query = InputQuery;
            if (specifications.Criteria is not  null)
            {
                query = query.Where(specifications.Criteria);
            }
            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }
            else if (specifications.OrderByAscending is not null)
            {
                query = query.OrderBy(specifications.OrderByAscending);
            }

            // Include related entities
            if (specifications.IncludeExpressions != null && specifications.IncludeExpressions.Count > 0)
                query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));
            
                return query;
        }
    }
}
