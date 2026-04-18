

using EBook.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBook.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task CreateCategory(Category category);
        Task<Category> GetCategoryById(int? id);
        Task UpdateCategory(Category category);
        Task<bool> DeleteCategory(int? id);
    }
}
