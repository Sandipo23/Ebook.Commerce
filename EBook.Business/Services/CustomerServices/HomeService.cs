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
    public class HomeService : IHomeService // this service is responsible for handling the business logic related to the home page of
                                            // the customer area, such as retrieving products and managing the shopping cart.
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
            IEnumerable<Product> productList = await _unitOfWork.Product.GetAllAsync(includeProperties: "Category"); // this method retrieves all products from the database,
                                                                                                                     // including their associated category information.
                                                                                                                     // It is used to display the products on the home page of the customer area.

            return productList;
        } 

        public async Task<Cart> GetDetailsAsync(int productId) // this method retrieves the details of a specific product and creates a Cart object
                                                               // with the product information. It is used when a user wants to add a product to their shopping cart.
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

        private async Task<string> GetUserId() // this method retrieves the user ID of the currently authenticated user
                                               // from the HTTP context. It is used to associate the shopping cart with the correct user when saving the cart details.
        {
            var claimsIdentity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public async Task<bool> SaveDetailsAsync(Cart cart) // this method saves the details of a product to the shopping cart.
                                                            // It checks if the user is authenticated, retrieves the user ID,
                                                            // and then either adds a new cart entry or updates an existing one in the database.
                                                            // It also updates the session with the current count of items in the cart.
        {
            string userId = await GetUserId();
            if (userId == null)
                return false;

            cart.AppUserId = userId;

            var cartDb = await _unitOfWork.Cart.GetFirstOrDefaultAsync(
                p => p.AppUserId == userId && p.ProductId == cart.ProductId); // this line checks if there is already an entry in the cart
                                                                              // for the current user and the specified product. If an entry exists,
                                                                              // it will be updated; otherwise, a new entry will be created.

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

            int cartCount = (await _unitOfWork.Cart.GetAllAsync(u => u.AppUserId == userId)).Count(); // this line retrieves all cart items for the current user and counts them to update
                                                                                                      // the session with the total number of items in the cart.
            _httpContextAccessor.HttpContext.Session.SetInt32("SessionCartCount", cartCount); // this line updates the session variable "SessionCartCount" with the current count
                                                                                              // of items in the cart, which can be used to display the cart count in the user interface.

            return true;
        }




    }
}
