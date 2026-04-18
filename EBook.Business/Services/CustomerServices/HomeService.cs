using Ebook.Common.Models.Entities;
using EBook.Data.Interfaces;
using Ecommerce.Application.Interfaces;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Business.Services.CustomerServices
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> productList = await _unitOfWork.Product.GetAllAsync(includeProperties: "Category");
            return productList;
        } 

        public async Task<Cart> GetDetailsAsync(int productId)
        {
            Cart cart = new Cart()
            {
                Count = 1,
                ProductId = productId,
                Product = await _unitOfWork.Product
                .GetFirstOrDefaultAsync(p => p.Id == productId, includeProperties: "Category"),
            };

            return cart;
        }

        private async Task<string> GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public async Task<bool> SaveDetailsAsync(Cart cart)
        {
            string userId = await GetUserId();
            if (userId == null)
                return false;

            cart.AppUserId = userId;

            var cartDb = await _unitOfWork.Cart.GetFirstOrDefaultAsync(
                p => p.AppUserId == userId && p.ProductId == cart.ProductId);

            if (cartDb == null)
            {
                await _unitOfWork.Cart.AddAsync(cart);
            }
            else
            {
                cartDb.Count += cart.Count;
                await _unitOfWork.Cart.UpdateAsync(cartDb);
            }

            await _unitOfWork.SaveAsync();

            int cartCount = (await _unitOfWork.Cart.GetAllAsync(u => u.AppUserId == userId)).Count();
            _httpContextAccessor.HttpContext.Session.SetInt32("SessionCartCount", cartCount);

            return true;
        }




    }
}
