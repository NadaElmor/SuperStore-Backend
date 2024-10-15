using Store.Core.Entities;
using SuperStore.Core.Repositery.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IGenericRepositery<T> Repositery<T>() where T :BaseEntity;
        Task<int> CompleteAsync();
    }
}
