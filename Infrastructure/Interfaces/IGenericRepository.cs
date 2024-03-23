using Core.Entities;
using Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IGenericRepository <T> where T : BaseEntity

    {
        //only mutual methods
        Task <T> GetByIdAsync(int? id);
       Task <IEnumerable<T>> GetAllAsync();

        Task<T> GetEntityWithSpecificationAsync(ISpecifications<T> specs);

        Task<IEnumerable<T>> GetAllWithSpecificationsAsync(ISpecifications<T> specs);
        Task<int> CountAsync(ISpecifications<T> specifications);
        Task Add(T entity);

        void Update(T entity);

        void Delete(T entity);


    }
}
