using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Specifications
{
    public class BaseSpecifications<T> : Ispecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Critria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get ; set; }=new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get ; set; }
        public int Skip { get ; set ; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecifications()
        {
            
        }
        public BaseSpecifications(Expression<Func<T, bool>> critira)
        {
            Critria = critira;
        }
      
        public void SetOrderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }
        public void SetOrderByDesc(Expression<Func<T, object>> OrderByDesc)
        {
            this.OrderByDesc = OrderByDesc;
        }
        public void ApplyPagination(int skip ,int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
