using EBook.Business.Interfaces;
using EBook.Business.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Store.Web.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        private OrderVM OrderVM;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
        }


        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.DetailsAsync(id);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Delivered(OrderVM orderVM)
        {
            var order = await _orderService.DeliveredAsync(orderVM);
            return RedirectToAction("Details", "Order", new { Id = order.OrderProduct.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(OrderVM orderVM)
        {
            var order = await _orderService.CancelOrderAsync(orderVM);
            return RedirectToAction("Details", "Order", new { Id = order.OrderProduct.Id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetails(OrderVM orderVM)
        {
            var order = await _orderService.UpdateOrderDetailsAsync(orderVM);
            return RedirectToAction("Details", "Order", new { Id = order.OrderProduct.Id });
        }


    }
}
