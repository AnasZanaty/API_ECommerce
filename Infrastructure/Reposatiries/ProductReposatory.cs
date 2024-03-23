using Core;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reposatiries
{
    public class ProductReposatory : IProductReposatory
    {

        private readonly StoreDbContext context;

        //contex constructor
        public ProductReposatory(StoreDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        
         =>await context.Set<Product>().ToListAsync();
        

        public async Task<Product> GetProductByIdAsync(int? id)

                    //=> await context.Set<Product>().FindAsync(id);

            => await context.Set<Product>().FirstOrDefaultAsync(i => i.Id == id);
        public async Task<IEnumerable<ProductBrand>> GetProductsBrandsAsync()
        
         =>await context.Set<ProductBrand>().ToListAsync();

        

        public async Task<IEnumerable<ProductType>> GetProductsTypeAsync()
        
        =>await context.Set<ProductType>().ToListAsync();
        
    }
}
