using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.API.Extensions;
using ShopOnline.API.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        public ShoppingCartController(IProductRepository productRepository, 
            IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await _shoppingCartRepository.GetItems(userId);
                if (cartItems == default)
                    return NoContent();

                var products = await _productRepository.GetItems();

                if (products == default)
                    throw new Exception("No product found");

                return Ok(cartItems.ConvertToDto(products));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.GetItem(id);
                if (cartItem == default)
                    return NotFound();

                var product = await _productRepository.GetItem(cartItem.ProductId);
                if (product == default)
                    return NotFound();

                return Ok(cartItem.ConvertToDto(product));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAdd)
        {
            try
            {
                var newCartItem = await _shoppingCartRepository.AddItem(cartItemToAdd);
                if(newCartItem == default)
                    return NoContent();

                var product = await _productRepository.GetItem(newCartItem.ProductId);
                if (product == default)
                    throw new Exception($"Error occured getting product");

                var newCartItemDto = newCartItem.ConvertToDto(product);

                return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);
                //return Ok(newCartItem.ConvertToDto(product));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
