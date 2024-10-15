using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public string? PaymentInentId { get; set; }
        public string? ClientSecret { get; set; }
        public List<BasketItem> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

        public CustomerBasket(string id)
        {
            this.Id = id;
        }

       
    }
}
