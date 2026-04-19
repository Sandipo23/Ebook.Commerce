using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Data.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Business.Services.AdminServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
       public CategoryService(IUnitOfWork unitOfWork) // Injecting the IUnitOfWork dependency through the constructor to ensure
                                                      // that the service has access to the necessary repositories for data operations.
                                                      // This promotes loose coupling and makes the service easier to test and maintain.
        {
            _unitOfWork = unitOfWork;
        } 

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            IEnumerable<Category> categoryList =  await _unitOfWork.Category.GetAllAsync();
            return categoryList;
        } 

        public async Task CreateCategory(Category category)
        {
            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.SaveAsync();
        } 

        public async Task<Category> GetCategoryById(int? id)
        {
            return await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCategory(Category category)
        {
            await _unitOfWork.Category.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
        } 

        public async Task<bool> DeleteCategory(int? id)
        {
            var category = await _unitOfWork.Category.GetFirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return false;
            }
            
                await _unitOfWork.Category.RemoveAsync(category);
                await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
