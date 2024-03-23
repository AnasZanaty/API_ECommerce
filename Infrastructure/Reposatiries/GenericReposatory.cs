using Core;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatiries
{
    public class GenericReposatory<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly StoreDbContext context;

        //contex constructor
        public GenericReposatory(StoreDbContext context)
        {
            this.context = context;
        }
        public async Task  Add(T entity)
    
        =>    await context.AddAsync(entity);
           
       

        public void Delete(T entity)
        =>  context.Remove(entity);


        public  async Task <IEnumerable<T>> GetAllAsync()
        =>await context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int? id)
        
         =>await context.Set<T>().FindAsync(id);

        public void Update(T entity)

          => context.Update(entity);

        public async Task<T> GetEntityWithSpecificationAsync(ISpecifications<T> specs)

            => await ApplySpecifications(specs).FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetAllWithSpecificationsAsync(ISpecifications<T> specs)
            => await ApplySpecifications(specs).ToListAsync();
      
        //private method to be called in tasks as in ISpecification returns GetQuery IQuerable not Tasks 
        private IQueryable<T> ApplySpecifications(ISpecifications<T> specs)

            =>  SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), specs);

        public async Task<int> CountAsync(ISpecifications<T> specifications)
        
            =>await ApplySpecifications(specifications).CountAsync();
        
    }
}
