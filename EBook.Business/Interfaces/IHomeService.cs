using EBook.Common.Entities;

using System.Collections.Generic;

namespace Ecommerce.Application.Interfaces
{
    public interface IHomeService
    {
        IEnumerable<Product> GetAllProducts();
        Cart GetDetails(int productId);
        bool SaveDetails(Cart cart);
    }
}
