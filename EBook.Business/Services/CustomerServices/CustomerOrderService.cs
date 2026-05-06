using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Data.Interfaces;


using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Business.Services.CustomerServices
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerOrderService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public async Task<IEnumerable<OrderProduct>> GetUserOrdersAsync()
        {
            string userId = GetUserId();
            if (userId == null)
                return new List<OrderProduct>();

            return await _unitOfWork.OrderProduct.GetAllAsync(u => u.AppUserId == userId);
        }

        public async Task CancelOrderAsync(int id) // this method will be called when the user clicks on the cancel button in the order details page,
                                                   // it will check if the order is not delivered yet, if it's not delivered it will change the order status to cancel,
                                                   // if it's delivered it will do nothing
        {
            string userId = GetUserId();
            if (userId == null)
            {
                return;
            }

            var order = await _unitOfWork.OrderProduct.GetFirstOrDefaultAsync(
                x => x.Id == id && x.AppUserId == userId);
            if (order == null)
            {
                return;
            }

            if (order.OrderStatus != "Delivered")
                order.OrderStatus = "Cancel";

            await _unitOfWork.OrderProduct.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
        }


    }
}
