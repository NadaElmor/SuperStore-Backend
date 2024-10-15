using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using SuperStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Repositery
{
    internal static class SpecificationsEvaluator<T>
        where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery, Ispecification<T> spec)
        {
            var query = InputQuery;
            //where 
            if(spec.Critria is not null)
            {
                query = query.Where(spec.Critria);
            }
            //include
            if(spec.Includes is not null)
            {
                query=spec.Includes.Aggregate(query,(CurrQuery,IncludeExp)=>CurrQuery.Include(IncludeExp));
            }
            //order by
            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if(spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            //skip , take 
            if (spec.IsPaginationEnabled == true)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }
            return query;
        }
    }
}
