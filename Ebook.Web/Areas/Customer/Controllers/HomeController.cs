
using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Store.Web
{
    [Area("Customer")]
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

        public async Task<IActionResult> Details(int productId)
        {
            Cart cart = await _homeService.GetDetailsAsync(productId);
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(Cart cart)
        {
            bool success = await _homeService.SaveDetailsAsync(cart);
            return RedirectToAction("Index");
        }
    }
}
