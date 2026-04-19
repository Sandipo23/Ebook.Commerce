using Ebook.Common.Models.Entities;
using EBook.Common.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBook.Business.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<ProductVM> GetProductVMAsync(int? id);
        Task UpSertProductAsync(ProductVM productVM, IFormFile file);
        Task DeleteProductAsync(int? id);
    }
}
