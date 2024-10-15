using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Entities.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, Address shippingAddress, ICollection<OrderItem> items, DeliveryMethod? deliveryMethod, decimal subTotal,string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            Items = items;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public Order(string buyerEmail, Address shippingAddress, ICollection<OrderItem> items, DeliveryMethod deliveryMethod, OrderStatus status, DateTimeOffset orderDate, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            Items = items;
            DeliveryMethod = deliveryMethod;
            Status = status;
            OrderDate = orderDate;
            SubTotal = subTotal;
           
        }

        public string BuyerEmail { get; set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> Items { get; set; }=new HashSet<OrderItem>();
        public DeliveryMethod? DeliveryMethod { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.UtcNow;
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; } 
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
    }
}
