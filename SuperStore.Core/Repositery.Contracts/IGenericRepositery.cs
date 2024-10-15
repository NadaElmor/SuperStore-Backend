using Store.Core.Entities;
using SuperStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Core.Repositery.Contracts
{
    public interface IGenericRepositery<T> where T : BaseEntity
    {
       public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<IReadOnlyList<T>> GetAllWihSpecAsync(Ispecification<T> spec);

        public Task<T?> GetByIdAsync(int id);
        public Task<T?> GetByIdWithSpecAsync(Ispecification<T> spec);
        public Task<int> GetCountForProducts(Ispecification<T> spec); 
        public Task AddAsync(T item);
        public void Update(T item);
        public void Delete(T item);
    }
}
