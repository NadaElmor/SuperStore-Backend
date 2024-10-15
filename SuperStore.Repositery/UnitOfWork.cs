using Store.Core.Entities;
using SuperStore.Core;
using SuperStore.Core.Repositery.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Repositery
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SuperStoreDbContext _dbcontext;

        private Hashtable _repositeries { get; set; }=new Hashtable();
        public UnitOfWork(SuperStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _dbcontext.DisposeAsync();
        }

        public IGenericRepositery<T> Repositery<T>() where T : BaseEntity
        {
            var Key = typeof(T).Name;
            if (!_repositeries.ContainsKey(Key))
            {
                var Repositery = new GenericRepositery<T>(_dbcontext);
                _repositeries.Add(Key, Repositery);
            }
            return _repositeries[Key] as IGenericRepositery<T>;
        }
    }
}
