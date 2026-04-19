using Ebook.Common.Models.Entities;
using System.Collections.Generic;

namespace EBook.Business.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Cart> GetDetailsAsync(int productId);
        Task<bool> SaveDetailsAsync(Cart cart);
    }
}
