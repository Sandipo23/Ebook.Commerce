using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Common.Models.ViewModel;
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
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartVM> GetCartForUserAsync(ClaimsPrincipal user)
        {
            var claimsIdentity = (ClaimsIdentity)user.Identity; // this line is to get the claims identity of the user, which contains the claims (like user ID, roles, etc.) associated with the authenticated user.
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return null;
            }

            var listCart = await _unitOfWork.Cart.GetAllAsync(
                p => p.AppUserId == claim.Value, includeProperties: "Product");

            var cartVM = new CartVM
            {
                ListCart = listCart,
                OrderProduct = new OrderProduct()
            };

            foreach (var cart in cartVM.ListCart)
            {
                cart.Price = cart.Product.Price * cart.Count;
                cartVM.OrderProduct.OrderPrice += cart.Price;
            }

            return cartVM;
        }
        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public async Task<CartVM> GetCartViewModelAsync()
        {
            string userId = GetUserId();
            if (userId == null)
                return null;

            var listCart = await _unitOfWork.Cart.GetAllAsync(p => p.AppUserId == userId, includeProperties: "Product");
            var appUser = await _unitOfWork.AppUser.GetFirstOrDefaultAsync(u => u.Id == userId);
            

            var cartVM = new CartVM
            {
                ListCart = listCart,
                OrderProduct = new OrderProduct
                {
                    AppUser = appUser,
                    Name = appUser.FullName,
                    CellPhone = appUser.CellPhone,
                    Address = appUser.Address,
                    PostalCode = appUser.PostalCode
                }
            };

            foreach (var cart in cartVM.ListCart)
            {
                cart.Price = cart.Product.Price * cart.Count;  //50 * 1 = 50 
                cartVM.OrderProduct.OrderPrice += cart.Price;  //= 250
            }

            return cartVM;
        }

        public async Task<bool> PlaceOrderAsync(CartVM cartVM)
        {
            string userId = GetUserId();
            if (userId == null)
                return false;

            var listCart = await _unitOfWork.Cart.GetAllAsync(p => p.AppUserId == userId, includeProperties: "Product");
            var appUser = await _unitOfWork.AppUser.GetFirstOrDefaultAsync(u => u.Id == userId);

            var orderProduct = new OrderProduct
            {
                AppUser = appUser,
                OrderDate = DateTime.Now,
                AppUserId = userId,
                Name = cartVM.OrderProduct.Name,
                CellPhone = cartVM.OrderProduct.CellPhone,
                Address = cartVM.OrderProduct.Address,
                PostalCode = cartVM.OrderProduct.PostalCode,
                OrderStatus = "Ordered",
                OrderPrice = listCart.Sum(c => c.Product.Price * c.Count)
            };

            await _unitOfWork.OrderProduct.AddAsync(orderProduct);
            await _unitOfWork.SaveAsync();

            foreach (var cart in listCart)
            {
                var orderDetails = new OrderDetails
                {
                    ProductId = cart.ProductId,
                    OrderProductId = orderProduct.Id,
                    Price = cart.Product.Price * cart.Count,
                    Count = cart.Count
                };
                await _unitOfWork.OrderDetails.AddAsync(orderDetails);
            }
            await _unitOfWork.SaveAsync();

            var carts = await _unitOfWork.Cart.GetAllAsync(u => u.AppUserId == orderProduct.AppUserId);
            await _unitOfWork.Cart.RemoveRangeAsync(carts);
            await _unitOfWork.SaveAsync();

            int cartCount = (await _unitOfWork.Cart.GetAllAsync(u => u.AppUserId == userId)).Count();
            _httpContextAccessor.HttpContext.Session.SetInt32("SessionCartCount", cartCount);

            return true;
        }




        public async Task<bool> IncreaseCartItemAsync(int cartId)
        {
            var cart = await _unitOfWork.Cart.GetFirstOrDefaultAsync(c => c.Id == cartId);
            if (cart == null)
                return false;

            cart.Count += 1;
            await _unitOfWork.Cart.UpdateAsync(cart);
            await _unitOfWork.SaveAsync();

            return true;
        }


        public async Task<bool> DecreaseCartItemAsync(int cartId)
        {
            var cart = await _unitOfWork.Cart.GetFirstOrDefaultAsync(c => c.Id == cartId);
            if (cart == null)
                return false;

            if (cart.Count > 1)
            {
                cart.Count -= 1;
                await _unitOfWork.Cart.UpdateAsync(cart);
            }
            else
            {
                await _unitOfWork.Cart.RemoveAsync(cart);
                var cartCount = (await _unitOfWork.Cart.GetAllAsync(u => u.AppUserId == cart.AppUserId)).Count() - 1; // decrease the cart count by 1 since we removed an item from the cart
                _httpContextAccessor.HttpContext.Session.SetInt32("SessionCartCount", cartCount);
            }

            await _unitOfWork.SaveAsync();
            return true;
        }


    }
}
