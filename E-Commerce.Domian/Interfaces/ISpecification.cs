using E_Commerce.Domian.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Interfaces
{
    public interface ISpecification<TEntity> where TEntity : BaseEntity
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludesExpressions { get; }
        public Expression<Func<TEntity, bool>> CriteriaExpression { get; }
        public Expression<Func<TEntity, object>>? OrderBy { get; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; }
        public int Take { get; }
        public int Skip { get; }
    }
}
