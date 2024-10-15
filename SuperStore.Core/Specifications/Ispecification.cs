using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Specifications
{
    public interface Ispecification<T> where T : BaseEntity
    {
        //signature for where(p=>p.id==id)
        public Expression<Func<T,bool>> Critria { get; set; }

        //List of signature for include(p=>p.productBrand)
        public List<Expression<Func<T,object>>> Includes { get; set; }
        //signature for order by
        public Expression<Func<T,Object>> OrderBy { get; set; }
        //signature for order by desc
        public Expression<Func<T, Object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
