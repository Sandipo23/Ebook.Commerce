
using EBook.Common.Models.ViewModel;
using System.Security.Claims;

namespace EBook.Business.Interfaces
{
    public interface ICartService
    {
        Task<CartVM> GetCartForUserAsync(ClaimsPrincipal user);
        Task<CartVM> GetCartViewModelAsync();
        Task<bool> PlaceOrderAsync(CartVM cartVM);
        Task<bool> IncreaseCartItemAsync(int cartId);
        Task<bool> DecreaseCartItemAsync(int cartId);
    }
}
