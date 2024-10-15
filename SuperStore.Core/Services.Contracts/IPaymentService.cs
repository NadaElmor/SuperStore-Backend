using SuperStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Services.Contracts
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentInent(string BasketId);
    }
}
