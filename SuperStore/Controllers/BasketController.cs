using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Entities;
using SuperStore.Core.Repositery.Contracts;
using SuperStore.DTOs;
using SuperStore.Errors;

namespace SuperStore.Controllers
{
   
    public class BasketController : BaseController
    {
        private readonly IBasketRepositery _basketRepositery;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepositery basketRepositery,IMapper mapper)
        {
            _basketRepositery = basketRepositery;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string Id)
        {
            var basket= await _basketRepositery.GetBasketAsync(Id);
            if (basket is null) return new CustomerBasket(Id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO Basket)
        {
            var MappedBasket=_mapper.Map<CustomerBasket>(Basket);
            var CreatedOrUpdatedBasket =await _basketRepositery.UpdateBasketAsync(MappedBasket);
            if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrUpdatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string Id)
        {
            await _basketRepositery.DeleteBasketAsync(Id);
        }
    }
}
