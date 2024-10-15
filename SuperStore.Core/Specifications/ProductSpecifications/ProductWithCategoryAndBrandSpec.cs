using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Specifications.ProductSpecifications
{
    public class ProductWithCategoryAndBrandSpec:BaseSpecifications<Product>
    {
        public ProductWithCategoryAndBrandSpec(ProductsParams productsParams) :base(p=>
            ((string.IsNullOrEmpty(productsParams.search)||(p.Name.ToLower().Contains(productsParams.search)))&&
            (!(productsParams.BrandId.HasValue)||(productsParams.BrandId.Value==p.ProductBrandId))&&((!productsParams.TypeId.HasValue)||(p.ProductTypeId==productsParams.TypeId.Value))
            ))
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            //order by
            switch (productsParams.Sort)
            {
                case "Sort":
                    SetOrderBy(p => p.Price);
                    break;
                case "sortDesc":
                    SetOrderByDesc(p => p.Price);
                    break;
                default:
                    SetOrderBy(p => p.Name);
                    break;

            }
            //pagination
            ApplyPagination(productsParams.pageSize * (productsParams.pageIndex - 1), productsParams.pageSize);
        }

        public ProductWithCategoryAndBrandSpec(int Id):base(p=>p.Id==Id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
        
    }
}
