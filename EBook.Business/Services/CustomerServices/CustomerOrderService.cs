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

        public async Task CancelOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderProduct.GetFirstOrDefaultAsync(x => x.Id == id);

            if (order.OrderStatus == "Ordered")
                order.OrderStatus = "Cancel";

            await _unitOfWork.OrderProduct.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
        }


    }
}
