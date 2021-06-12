using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Api.Dtos;
using Skinet.Api.Errors;
using Skinet.Api.Helpers;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.Api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductType> typesRepo,
            IMapper mapper)
        {
            _productsRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.ListAllAsync(spec);

            var data = _mapper.Map<List<ProductToReturnDto>>(products);

            var response = new Pagination<ProductToReturnDto>(
                productParams.PageIndex,
                productParams.PageSize,
                totalItems,
                data);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product == null)
                return NotFound(new ApiResponse(404));

            var productToReturn = _mapper.Map<ProductToReturnDto>(product);

            return Ok(productToReturn);
        }

        //[HttpGet]
        //[Route("brands")]
        //public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        //{
        //    var brands = await _brandsRepo.ListAllAsync();
        //    return Ok(brands);
        //}

        //[HttpGet]
        //[Route("types")]
        //public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        //{
        //    var types = await _typesRepo.ListAllAsync();
        //    return Ok(types);
        //}
    }
}
