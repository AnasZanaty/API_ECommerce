using Core.Entities;
using Infrastructure.Specifications;
using Services.Helper;
using Services.ProductServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductServices
{
    public interface IProductService
    {
        Task<ProductResultDto> GetProductByIdAsync(int? id);
        Task<Pagination<ProductResultDto>> GetAllProductsAsync(ProductSpecifications specifications);
        Task<IEnumerable<ProductType>> GetProductsTypeAsync();
        Task<IEnumerable<ProductBrand>> GetProductsBrandsAsync();


    }
}
