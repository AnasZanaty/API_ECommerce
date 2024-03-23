using AutoMapper;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.AspNetCore.Mvc;
using Services.Helper;
using Services.ProductServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Pagination<ProductResultDto>> GetAllProductsAsync(ProductSpecifications specifications)
        {
            var specs = new ProductTypesAndBrandsSpecifications(specifications);
            var products = await unitOfWork.Repository<Product>().GetAllWithSpecificationsAsync(specs);
            var totalItems = await unitOfWork.Repository<Product>().CountAsync(specs);
            var mappedproducts= mapper.Map<IEnumerable<ProductResultDto>>(products);
            
            return new Pagination<ProductResultDto>(specifications.PageIndex , specifications.PageSize ,totalItems , mappedproducts);
        }


        public async Task<ProductResultDto> GetProductByIdAsync(int? id)

        {
            var specs = new ProductTypesAndBrandsSpecifications(id);
            var product = await unitOfWork.Repository<Product>().GetEntityWithSpecificationAsync(specs);

            var mappedProduct = mapper.Map<ProductResultDto>(product);
            return mappedProduct;
        }

        public async Task<IEnumerable<ProductBrand>> GetProductsBrandsAsync()
                 => await unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<IEnumerable<ProductType>> GetProductsTypeAsync()
                => await unitOfWork.Repository<ProductType>().GetAllAsync();

    }
}
