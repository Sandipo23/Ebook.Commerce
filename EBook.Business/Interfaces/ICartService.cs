using EBook.Business.ViewModel;

using System.Security.Claims;

namespace EBook.Business.Interfaces
{
    public interface ICartService
    {
        CartVM GetCartForUserAsync(ClaimsPrincipal user);
        CartVM GetCartViewModelAsync();
        bool PlaceOrderAsync(CartVM cartVM);
        bool IncreaseCartItem(int cartId);
        bool DecreaseCartItem(int cartId);
    }
}
