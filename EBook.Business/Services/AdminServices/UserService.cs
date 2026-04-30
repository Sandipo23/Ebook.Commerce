using Ebook.Common.Models.Entities;
using EBook.Business.Interfaces;
using EBook.Common.Models.ViewModel;
using EBook.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EBook.Business.Services.AdminServices
{
    public class UserService : IUserService // this class is responsible for handling user-related operations
                                            // such as registration, login, and logout. It uses the UserManager and SignInManager services provided by ASP.NET Core Identity to manage user accounts and authentication.
    {
          private readonly UserManager<ApplicationUser> _userManager; // is a service provided by ASP.NET Core Identity framework that allows us to
                                                                    // manage user accounts, including creating, updating, deleting, and retrieving users from the database.
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(
            UserManager<ApplicationUser> userManager,
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

            var roleResult = await _userManager.AddToRoleAsync(user, "Customer"); // this line assigns the newly created user to the "Customer" role,
                                                                                  // which is a predefined role in the application that grants specific permissions
                                                                                  // and access rights to users assigned to it.
                                                                                  // By adding the user to the "Customer" role, we can control what actions and
                                                                                  // features they can access within the application based on their role.

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
