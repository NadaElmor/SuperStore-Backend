using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Entities;
using SuperStore.Core.Repositery.Contracts;
using SuperStore.Core.Specifications.ProductSpecifications;
using SuperStore.DTOs;
using SuperStore.Errors;
using SuperStore.Helper;

namespace SuperStore.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IGenericRepositery<Product> _productRepositery;
        private readonly IMapper _mapper;
        private readonly IGenericRepositery<ProductType> _categoryRepositery;
        private readonly IGenericRepositery<ProductBrand> _productBrandRepositery;

        public ProductsController(IGenericRepositery<Product> productRepositery, IMapper mapper, IGenericRepositery<ProductType> categoryRepositery, IGenericRepositery<ProductBrand> productBrandRepositery)
        {
            _productRepositery = productRepositery;
            _mapper = mapper;
            _categoryRepositery = categoryRepositery;
            _productBrandRepositery = productBrandRepositery;
        }
     //   [Authorize(AuthenticationSchemes ="Bearer")]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetAllProducts([FromQuery]ProductsParams productsParams) {
            var spec = new ProductWithCategoryAndBrandSpec(productsParams);
            var Products = await _productRepositery.GetAllWihSpecAsync(spec);
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(Products);
            var CountSpec = new ProductWithFilterationForCountSpecifications(productsParams);
            int Count=await _productRepositery.GetCountForProducts(CountSpec);

            return Ok(new Pagination<ProductToReturnDTO>(productsParams.pageIndex,productsParams.pageSize,Count,Data));
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetById(int Id)
        {
            var spec = new ProductWithCategoryAndBrandSpec(Id);
            var Product = await _productRepositery.GetByIdWithSpecAsync(spec);
            if (Product is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product, ProductToReturnDTO>(Product));
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes(){
            var ProductTypes =await _categoryRepositery.GetAllAsync();
            return Ok(ProductTypes);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var ProductBrands = await _productBrandRepositery.GetAllAsync();
            return Ok(ProductBrands);
        }

}
}
