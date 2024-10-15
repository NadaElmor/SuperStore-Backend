using Store.Core.Entities;
using SuperStore.Core;
using SuperStore.Core.Entities;
using SuperStore.Core.Entities.Order_Aggregate;
using SuperStore.Core.Repositery.Contracts;
using SuperStore.Core.Services.Contracts;
using SuperStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Services
{
    public class OrderServie : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepositery _basketRepositery;
        private readonly IPaymentService _paymentService;

        public OrderServie(IUnitOfWork unitOfWork,IBasketRepositery basketRepositery,IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketRepositery = basketRepositery;
            _paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            // 1- Get Basket from BasketRepo 
            var Basket = await _basketRepositery.GetBasketAsync(BasketId);

            // 2-Get Selected Items in Basket from ProductsRepo
            var OrderItems = new List<OrderItem>();
            if(Basket?.Items?.Count > 0)
            {
                var _ProductRepo = _unitOfWork.Repositery<Product>();
                foreach(var Item in Basket.Items)
                {
                    var Product = await _ProductRepo.GetByIdAsync(Item.Id);
                    var ProductItemOrder = new ProductItemOrdered(Item.Id,Product.Name,Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrder, Product.Price, Item.Quantity);

                    OrderItems.Add(OrderItem);
                }
            }

            // 3-Calculate Subtotal
            var SubTotal = OrderItems.Sum(o => o.Price * o.Quantity);

            // 4-GetDeliveryMethod
            var DeliveryMethod =await _unitOfWork.Repositery<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            //Check if there was order with the same PaymentIntentId
            var OrderRepo = _unitOfWork.Repositery<Order>();
            var Spec = new OrderForPaymentIntentIdSpec(Basket.PaymentInentId);
            var ExistingOrder = await OrderRepo.GetByIdWithSpecAsync(Spec);
            if(ExistingOrder !=null)
            {
                OrderRepo.Delete(ExistingOrder);
                await _paymentService.CreateOrUpdatePaymentInent(BasketId);
            }    


            // 5-Create Order
            var Order = new Order(buyerEmail, ShippingAddress, OrderItems, DeliveryMethod, SubTotal,Basket.PaymentInentId);
            await _unitOfWork.Repositery<Order>().AddAsync(Order);

            // 6-Save Changes
            var Result = await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null;
            return Order;
        }

        public async Task<IReadOnlyList<Order>?> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var Spec = new OrderSpecifications(BuyerEmail);
            var Orders=await _unitOfWork.Repositery<Order>().GetAllWihSpecAsync(Spec);
            return Orders;
        }

        public async Task<Order> GetOrderByIdForUserAsync(string BuyerEmail, int OrderId)
        {
            var Spec = new OrderSpecifications(BuyerEmail, OrderId);
            var Orders=await _unitOfWork.Repositery<Order>().GetByIdWithSpecAsync(Spec);
            return Orders;
        }
    }
}
