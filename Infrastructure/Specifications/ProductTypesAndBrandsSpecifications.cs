using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    //Specification pattern -FORTH STEP-
    public class ProductTypesAndBrandsSpecifications : BaseSpecifications<Product>
    {
        //1st constructor for specification needed to be added in
        public ProductTypesAndBrandsSpecifications(ProductSpecifications specifications) : 
            base(x=>
                 (string.IsNullOrEmpty(specifications.Search) || x.Name.Trim().ToLower().Contains(specifications.Search))
                 && 
                 (!specifications.BrandId.HasValue || x.ProductBrandId == specifications.BrandId) 
                 && 
                 (!specifications.TypeId.HasValue || x.ProductTypeId == specifications.TypeId)
                )

        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            AddOrderBy(p => p.Name);
            ApplyPagination(specifications.PageSize * (specifications.PageIndex - 1), specifications.PageSize);

            if (!string.IsNullOrEmpty(specifications.Sort))
            {
                switch (specifications.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }

            }

        }
        //2nd constructor for specification by id 

        public ProductTypesAndBrandsSpecifications(int ? id) :
           base(x =>x.Id == id )


        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
    }


}
