using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using SuperStore.Core.Repositery.Contracts;
using SuperStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Repositery
{
    public class GenericRepositery<T> : IGenericRepositery<T> where T : BaseEntity
    {
        private readonly SuperStoreDbContext _superStoreDbContext;

        public GenericRepositery(SuperStoreDbContext superStoreDbContext)
        {
            _superStoreDbContext = superStoreDbContext;
        }

        public async Task AddAsync(T item)
        {
            await _superStoreDbContext.AddAsync(item);
        }

        public async void Delete(T item)
        {
             _superStoreDbContext.Remove(item);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _superStoreDbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWihSpecAsync(Ispecification<T> Spec)
        {
            return await ApplySpec(Spec).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _superStoreDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdWithSpecAsync(Ispecification<T> Spec)
        {
            return await ApplySpec(Spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountForProducts(Ispecification<T> spec)
        {
            return await ApplySpec(spec).CountAsync();
        }

        public void Update(T item)
        {
            _superStoreDbContext.Update(item);
        }

        private IQueryable<T> ApplySpec(Ispecification<T> Spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_superStoreDbContext.Set<T>(), Spec);
        }
    }
}
