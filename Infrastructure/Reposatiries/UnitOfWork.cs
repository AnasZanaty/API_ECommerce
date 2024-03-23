using Core;
using Core.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatiries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;
        private Hashtable reposatories;

        //HashTables has Key and Value the key is for the type or  name of  Entity or reposatory and the value is for the instance of reposatory(object)

        public UnitOfWork(StoreDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Complete()
        => await context.SaveChangesAsync();

        public void Dispose()
             => context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (reposatories is null)
            {
                reposatories = new Hashtable();
            }
                var type = typeof(TEntity).Name;

                if (!reposatories.ContainsKey(type))
                {
                    //can't make an instance or object by the normal way
                    
                    var reposatoryType = typeof(GenericReposatory<>);
                    var reposatoryInstance = Activator.CreateInstance(reposatoryType.MakeGenericType(typeof(TEntity)), context);

                  reposatories.Add(type, reposatoryInstance);
                }

                return (IGenericRepository<TEntity>)reposatories[type];
            

        }
    }
}
