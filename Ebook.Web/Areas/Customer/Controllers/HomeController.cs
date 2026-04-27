
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

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Product> productList = await _homeService.GetAllProductsAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                productList = productList.Where(p => p.Name != null && p.Name.ToLower().Contains(searchString.ToLower()));
            }

            ViewData["CurrentFilter"] = searchString; // this line is used to keep the search string in the search box after the search is performed.
                                                      // It will be used in the view to set the value of the search box.

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
