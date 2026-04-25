using EBook.Business.Interfaces;
using EBook.Common.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Store.Web.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
      
            private readonly IProductService _productService;

            public ProductController(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<IActionResult> Index()
            {
                var productList = await _productService.GetAllProductsAsync();
                return View(productList);
            }

            public async Task<IActionResult> Upsert(int? id)
            {
                var vm = await _productService.GetProductVMAsync(id);
                return View(vm);
            }

            [HttpPost]
            public async Task<IActionResult> Upsert(ProductVM vm, IFormFile file)
            {
                if (!ModelState.IsValid)
                {
                    var refreshedVM = await _productService.GetProductVMAsync(vm.Product.Id > 0 ? vm.Product.Id : null);
                    vm.CategoryList = refreshedVM.CategoryList;
                    return View(vm);
                }
                try
                {
                    await _productService.UpSertProductAsync(vm, file);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("Product.CategoryId", ex.Message);
                    var refreshedVM = await _productService.GetProductVMAsync(vm.Product.Id > 0 ? vm.Product.Id : null);
                    vm.CategoryList = refreshedVM.CategoryList;
                    return View(vm);
                }
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || id <= 0) return NotFound();
                await _productService.DeleteProductAsync(id.Value);
                return RedirectToAction(nameof(Index));
            }

        }
    }
