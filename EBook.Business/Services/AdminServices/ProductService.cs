using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Data.Interfaces;

using Ecommerce.Application.ViewModels;

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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private string _wwwRootPath;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> productList = await _unitOfWork.Product.GetAllAsync();
            return productList;
        }

        public async Task<ProductVM> GetProductVMAsync(int? id)
        {
            ProductVM productVM = new ProductVM() // this line is creating a new instance of the ProductVM class and initializing its properties.
                                                  // The Product property is set to a new instance of the Product class, and the CategoryList property is
                                                  // populated with a list of SelectListItem objects created from the categories retrieved from the database
                                                  // using the _unitOfWork.Category.GetAllAsync() method.
                                                  // Each SelectListItem has its Text property set to the category name
                                                  // and its Value property set to the category ID as a string.
            {
                Product = new Product(),
                CategoryList = (await _unitOfWork.Category.GetAllAsync())
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            if (id.HasValue && id > 0)
            {
                productVM.Product = await _unitOfWork.Product.GetFirstOrDefaultAsync(p => p.Id == id);
            }
            return productVM;
        }

        public async Task UpSertProductAsync(ProductVM productVM, IFormFile file)
        {

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
          
            await _unitOfWork.Product.RemoveAsync(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
