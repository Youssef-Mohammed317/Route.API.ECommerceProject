using E_Commerce.Domian.Entites;
using E_Commerce.Domian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Specifications
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludesExpressions { get; } = [];
        public Expression<Func<TEntity, bool>> CriteriaExpression { get; }
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
        public int Take { get; private set; } = 10;
        public int Skip { get; private set; } = 0;

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteriaExpression)
        {
            CriteriaExpression = criteriaExpression;
        }
        protected void AddInclude(Expression<Func<TEntity, object>> includeExp)
        {
            IncludesExpressions.Add(includeExp);
        }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp)
        {
            OrderBy = orderByExp;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExp)
        {
            OrderBy = orderByDescendingExp;
        }
        protected void ApplyPagination(int pageSize = 10, int pageIndex = 0)
        {
            Skip = pageSize * (pageIndex - 1);
            Take = pageSize;
        }
    }
}
