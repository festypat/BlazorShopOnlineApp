using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<IEnumerable<CartItemDto>> GetItems(int userId);
    }
}
