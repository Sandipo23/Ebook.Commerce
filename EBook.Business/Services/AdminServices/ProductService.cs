using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Common.ViewModel;
using EBook.Data.Interfaces;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Business.Services.AdminServices
{
    public class ProductService : IProductService // this class implements the IProductService interface,
    {
        private readonly IUnitOfWork _unitOfWork;
      
        private string _wwwRootPath;
        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _wwwRootPath = env.WebRootPath;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> productList = await _unitOfWork.Product.GetAllAsync();
            return productList;
        }

        public async Task<ProductVM> GetProductVMAsync(int? id) 
        {
            ProductVM productVM = new ProductVM() 
            {
                Product = new Product(),
                CategoryList = (await _unitOfWork.Category.GetAllAsync())
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            if (id.HasValue && id > 0) // this line checks if the id parameter has a value and if that value is greater than 0.
                                       // then it retrieves the first product from the database that matches the given id and assigns
                                       // it to the Product property of the ProductVM instance. This allows the method to return a
                                       // ProductVM that contains the details of the specified product along with a list of categories for selection.
            {
                productVM.Product = await _unitOfWork.Product.GetFirstOrDefaultAsync(p => p.Id == id);
            }
            return productVM;
        }

        public async Task UpSertProductAsync(ProductVM productVM, IFormFile file)
        {
            var categoryExists = await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == productVM.Product.CategoryId);
            if (categoryExists == null)
            {
                throw new InvalidOperationException("The selected category does not exist. Please choose a valid category.");
            }

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploadRoot =Path.Combine(_wwwRootPath, "img","products");
                var extension = Path.GetExtension(file.FileName);

                if(!string.IsNullOrEmpty(productVM.Product.Picture)) // this line checks if the Picture property of the Product object within the ProductVM instance
                                                                     // is not null or empty.
                {
                    var oldImagePath = Path.Combine(_wwwRootPath, productVM.Product.Picture);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                } 

                using (var fileStream = new FileStream(Path.Combine(uploadRoot, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.Picture = Path.Combine(@"\img", "products", fileName + extension);
            }
            if (productVM.Product.Id <= 0)
            {
                // Create
                await _unitOfWork.Product.AddAsync(productVM.Product);
            }
            else
            {
                // Update
                await _unitOfWork.Product.UpdateAsync(productVM.Product);
            }
            await _unitOfWork.SaveAsync();
        }


        public async Task DeleteProductAsync(int? id)
        {
            var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                await _unitOfWork.Product.RemoveAsync(product);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
