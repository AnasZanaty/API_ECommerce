using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecifications<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecifications specifications) :
    base(x =>
         (string.IsNullOrEmpty(specifications.Search) || x.Name.Trim().ToLower().Contains(specifications.Search)) 
         && 

         (!specifications.BrandId.HasValue || x.ProductBrandId == specifications.BrandId)
         &&
         (!specifications.TypeId.HasValue || x.ProductTypeId == specifications.TypeId)
        )
        {

        }
    }
}
