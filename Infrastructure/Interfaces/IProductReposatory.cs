using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IProductReposatory
    {
        Task<Product> GetProductByIdAsync(int? id);

        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<ProductType>> GetProductsTypeAsync();
        Task<IEnumerable<ProductBrand>> GetProductsBrandsAsync();

    }
}
