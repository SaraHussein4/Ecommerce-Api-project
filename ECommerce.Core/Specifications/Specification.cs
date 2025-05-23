using ECommerce.Core.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Specifications
{
    public class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get ; set; }
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set ; }
        public bool IsPaginationEnabled { get ; set ; }

        public Specification()
        {
            Includes = new List<Expression<Func<T, object>>>();
        }
        public Specification(Expression<Func<T,bool>> CriteriaExpression)
        {
            this.Criteria = CriteriaExpression;
            Includes = new List<Expression<Func<T, object>>>();
        }

        public void AddOrderBy(Expression<Func<T,object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDescending = OrderByDescExpression;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
