
using EBook.Business.Interfaces;
using EBook.Common.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Store.Web
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ICartService _cartSerivce;
        public CartVM CartVM { get; set; }

        public CartController(ICartService cartSerivce)
        {
            _cartSerivce = cartSerivce;

        }

        public IActionResult Index()
        {
            var cartVM = _cartSerivce.GetCartForUserAsync(User);
            if (cartVM == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(cartVM);
        }

        public IActionResult Order()
        {
            var cartVM = _cartSerivce.GetCartViewModelAsync();
            if (cartVM == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(cartVM);
        }

        [HttpPost]
        [ActionName("Order")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderPost(CartVM cartVM)
        {
            bool success =await _cartSerivce.PlaceOrderAsync(cartVM);
            if (!success)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction(nameof(Index), "Home", new { area = "Customer" });
        }

        public async Task<IActionResult> Increase(int cartId)
        {
            bool success = await _cartSerivce.IncreaseCartItemAsync(cartId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Decrease(int cartId)
        {
            bool success = await _cartSerivce.DecreaseCartItemAsync(cartId);
            return RedirectToAction(nameof(Index));
        }
    }
}
