using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.API.Extensions;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            var products = await _productRepository.GetItems();
            var productCategories= await _productRepository.GetCategories();

            if (products == default || productCategories == default)
                return NotFound();

            return Ok(products.ConvertToDto(productCategories));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            var product = await _productRepository.GetItem(id);
            var productCategories = await _productRepository.GetCategories();

            if (product == default)
                return BadRequest();

            var productCategory = await _productRepository.GetCategory(product.CategoryId);

            return Ok(product.ConvertToDto(productCategory));
        }
    }
}
