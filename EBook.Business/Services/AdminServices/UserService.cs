using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Business.ViewModel;

using Microsoft.AspNetCore.Identity;

namespace EBook.Business.Services.AdminServices
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Address = model.Address,
                PostalCode = model.PostalCode,
                CellPhone = model.CellPhone
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return result;

            //Customer

            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

            if (!roleResult.Succeeded)
                return IdentityResult.Failed(roleResult.Errors.ToArray());


            return result;
        }

        public Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return _signInManager
                .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        }

        public Task LogoutAsync()
            => _signInManager.SignOutAsync();
    }
}
