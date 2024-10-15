using Microsoft.Extensions.Configuration;
using Store.Core.Entities;
using Stripe;
using SuperStore.Core;
using SuperStore.Core.Entities;
using SuperStore.Core.Entities.Order_Aggregate;
using SuperStore.Core.Repositery.Contracts;
using SuperStore.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Core.Entities.Product;



namespace SuperStore.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepositery _basketRepositery;

        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork,IBasketRepositery basketRepositery)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketRepositery = basketRepositery;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentInent(string BasketId)
        {
            // set secret key
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            //Get the Basket
            var Basket= await _basketRepositery.GetBasketAsync(BasketId);
            if(Basket == null) { return null; }

            //check if the Price in Basket is correct
            if (Basket.Items.Count > 0)
            {
                var _ProductRepo = _unitOfWork.Repositery<Product>();
                foreach (var item in Basket.Items)
                {
                    var Product = await _ProductRepo.GetByIdAsync(item.Id);
                    item.Price = Product.Price;
                }
            }

            //Get DeliveryMethod
            var ShippingPrice = 0m;
            if (Basket.DeliveryMethodId.HasValue)
            {
                var _DeliveryMethodRepo = _unitOfWork.Repositery<DeliveryMethod>();
                var DeliveryMethod =await _DeliveryMethodRepo.GetByIdAsync(Basket.DeliveryMethodId.Value);
                Basket.ShippingPrice = DeliveryMethod.Cost;
                ShippingPrice = DeliveryMethod.Cost;
            }

            //PaymentIntent
            PaymentIntent paymentIntent;
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            //if there is no paymentInentId , create new one 
            if (string.IsNullOrEmpty(Basket.PaymentInentId))
            {
                var Options =new PaymentIntentCreateOptions()
                {
                    Amount =(long)Basket.Items.Sum(P=>P.Price*100*P.Quantity)+(long)ShippingPrice*100,
                    Currency="USD",
                    PaymentMethodTypes=new List<string>() { "Card"}
                };
                //Integrate with stripe
                paymentIntent = await paymentIntentService.CreateAsync(Options);
                //update basket with the new PaymentId and ClientSecret
                Basket.PaymentInentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                //update existing paymentIntentId
                var UpdatedOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Basket.Items.Sum(P => P.Price * 100 * P.Quantity) + (long)ShippingPrice * 100
                };
                await paymentIntentService.UpdateAsync(Basket.PaymentInentId, UpdatedOptions);
            }

            //Update the Basket
            await _basketRepositery.UpdateBasketAsync(Basket);

            return Basket;
        }
    }
}
