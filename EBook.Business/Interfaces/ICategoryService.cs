

using EBook.Common.Entities;
using System.Collections.Generic;

namespace EBook.Business.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        void CreateCategory(Category category);
        Category GetCategoryById(int? id);
        void UpdateCategory(Category category);
        bool DeleteCategory(int? id);
    }
}
