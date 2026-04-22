using EBook.Business.Services.AdminServices;
using EBook.Business.Interfaces;
using EBook.Common.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Store.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.RegisterAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            foreach (var err in result.Errors)
                ModelState.AddModelError("", err.Description);

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) { 
        
            if (!ModelState.IsValid) return View(model);

            var result = await _userService.LoginAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home", new { area = "Customer" });

            
                ModelState.AddModelError("", "Invalid username or password.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
