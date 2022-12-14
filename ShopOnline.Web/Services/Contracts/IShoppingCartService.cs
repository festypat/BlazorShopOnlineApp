using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<List<CartItemDto>> GetItems(int userId);
        Task<CartItemDto> DeleteItem(int id);
    }
}
