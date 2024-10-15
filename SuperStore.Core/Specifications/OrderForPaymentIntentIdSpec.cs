using SuperStore.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Specifications
{
    public class OrderForPaymentIntentIdSpec:BaseSpecifications<Order>
    {
        public OrderForPaymentIntentIdSpec(string PaymentIntentId):base(O=>O.PaymentIntentId==PaymentIntentId) 
        { }
    }
}
