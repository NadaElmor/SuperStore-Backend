using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Entities;
using SuperStore.Core.Services.Contracts;
using SuperStore.Errors;

namespace SuperStore.Controllers
{
   
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            paymentService = paymentService;
        }

        [HttpPost("{BasketId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await _paymentService.CreateOrUpdatePaymentInent(BasketId);
            if (Basket is null) return BadRequest(new ApiResponse(400, "Basket is not found"));
            return Ok(Basket);
        }
    }
}
