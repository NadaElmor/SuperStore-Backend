using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Specifications.ProductSpecifications
{
    public class ProductsParams
    {
        //string? Sort,int? BrandId,int? TypeId
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private int PageSize=5;

        public int pageSize
        {
            get { return PageSize=5; }
            set { PageSize = value>MaxPageSize?10:value; }
        }
        private const int  MaxPageSize= 10;
        private int PageIndex=1;

        public int pageIndex
        {
            get { return PageIndex; }
            set { PageIndex = value; }
        }

        //search
        private string? Search;

        public string? search
        {
            get { return Search; }
            set { Search = value.ToLower(); }
        }

    }
}
