using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ProductServices;
using Services.ProductServices.Dto;
using Services.Helper;
using Demo.HandleResponses;
using Demo.Helper;

namespace Demo.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductResultDto>>> GetProducts([FromQuery] ProductSpecifications specifications)
        {
            var products = await productService.GetAllProductsAsync(specifications);

            return Ok(products);
        }
        [HttpGet]
        [Route("Brands")]

        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
            => Ok(await productService.GetProductsBrandsAsync());

        [HttpGet("Types")]
        public async Task<ActionResult <IEnumerable<ProductType>>> GetProductTypes()
           =>Ok( await productService.GetProductsTypeAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status410Gone)]
        [Cache(100)]

        public async Task<ActionResult<ProductResultDto>> GetProductById(int? id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(product);


        }
    }


    }
