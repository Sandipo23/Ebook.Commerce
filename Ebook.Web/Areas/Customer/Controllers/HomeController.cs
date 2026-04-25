
using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Store.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;// this service will be used to get the products and details of the products

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> productList = await _homeService.GetAllProductsAsync();

            return View(productList);
        } 

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Cart cart = await _homeService.GetDetailsAsync(id);
            if (cart?.Product == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(Cart cart)
        {
            cart.Id = 0; // The route data 'id' maps to ProductId but MVC model binding assigns it to Cart.Id. We must reset it to 0 so EF Core treats it as a new entity.
            bool success = await _homeService.SaveDetailsAsync(cart);
            return RedirectToAction(nameof(Index), "Cart");
        }
    }
}
