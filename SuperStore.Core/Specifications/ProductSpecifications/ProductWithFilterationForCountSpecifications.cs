using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Specifications.ProductSpecifications
{
    public class ProductWithFilterationForCountSpecifications:BaseSpecifications<Product>
    {
        public ProductWithFilterationForCountSpecifications(ProductsParams productsParams):base(
            p =>
            ((string.IsNullOrEmpty(productsParams.search) || (p.Name.ToLower().Contains(productsParams.search))) &&
            (!(productsParams.BrandId.HasValue) || (productsParams.BrandId.Value == p.ProductBrandId)) && ((!productsParams.TypeId.HasValue) || (p.ProductTypeId == productsParams.TypeId.Value))
            ))
            
        {
            
        }
    }
}
