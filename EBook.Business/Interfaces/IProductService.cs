using EBook.Common.Entities;
using Ecommerce.Application.ViewModels;

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EBook.Business.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        ProductVM GetProductVM(int? id);
        void UpSertProduct(ProductVM productVM, IFormFile file);
        void DeleteProduct(int? id);
    }
}
