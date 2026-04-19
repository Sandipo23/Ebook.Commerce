using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Store.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categoryList = await _categoryService.GetAllCategories();  // this will return a list of categories from the database
                                                                                             // and IEnumerable<Category> will be used to store the list of categories and then
                                                                                             // we will pass this list to the view and in the view we will display the list of categories in a table format.
            return View(categoryList);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategory(category);
                return RedirectToAction(nameof(Index));  // this will redirect the user to the index page after the category is updated successfully.
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            bool isDeleted = await _categoryService.DeleteCategory(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
