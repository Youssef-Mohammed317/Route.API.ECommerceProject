using E_Commerce.Domian.Entites;
using E_Commerce.Domian.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    public static class SpecificationEvaluater
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,
            ISpecification<TEntity, TKey> specification) where TEntity : BaseEntity<TKey>
        {
            var query = EntryPoint;

            if (specification is not null)
            {
                if (specification.CriteriaExpression is not null)
                {
                    query = query.Where(specification.CriteriaExpression);
                }

                if (specification.IncludesExpressions?.Any() ?? false)
                {
                    foreach (var expression in specification.IncludesExpressions)
                    {
                        query = query.Include(expression);
                    }
                }
                if (specification.OrderBy is not null)
                {
                    query = query.OrderBy(specification.OrderBy);
                }
                if (specification.OrderByDescending is not null)
                {
                    query = query.OrderByDescending(specification.OrderByDescending);
                }

                query = query.Skip(specification.Skip).Take(specification.Take);

            }

            return query;
        }
        public static IQueryable<TEntity> CreateCountQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,
            ISpecification<TEntity, TKey> specification) where TEntity : BaseEntity<TKey>
        {
            var query = EntryPoint;

            if (specification is not null)
            {
                if (specification.CriteriaExpression is not null)
                {
                    query = query.Where(specification.CriteriaExpression);
                }

            }

            return query;
        }
    }
}
