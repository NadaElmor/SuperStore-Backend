using SuperStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Repositery.Contracts
{
    public interface IBasketRepositery
    {
        Task<CustomerBasket?> GetBasketAsync(string BasketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);
        Task<bool> DeleteBasketAsync(string BasketId); 
    }
}
