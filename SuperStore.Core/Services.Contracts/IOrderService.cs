using Store.Core.Entities;
using SuperStore.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Services.Contracts
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail,string BasketId,int DeliveryMethodId,Address ShippingAddress);
        Task<IReadOnlyList<Order>> GetAllOrdersForUserAsync(string BuyerEmail);
        Task<Order> GetOrderByIdForUserAsync(string BuyerEmail, int OrderId);

    }
}
