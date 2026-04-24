
using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Common.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICustomerOrderService _orderService;
        public OrderVM OrderVM { get; set; }

        public OrderController(ICustomerOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<OrderProduct> orderProducts = await _orderService.GetUserOrdersAsync();
            return View(orderProducts);
        }

        public async Task<IActionResult> CancelOrder(int id)
        {
            await _orderService.CancelOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
