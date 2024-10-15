using SuperStore.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Specifications
{
    public class OrderSpecifications :BaseSpecifications<Order>
    {
        public OrderSpecifications(string BuyerEmail):base(O=>O.BuyerEmail==BuyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.ShippingAddress);
            Includes.Add(o => o.Items);
            SetOrderByDesc(o => o.OrderDate);
        }
        public OrderSpecifications(string BuyerEmail ,int Id):base(
            o=>o.BuyerEmail==BuyerEmail&& o.Id==Id
            )
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.ShippingAddress);
            Includes.Add(o => o.Items);
            SetOrderByDesc(o => o.OrderDate);
        }
    }
}
