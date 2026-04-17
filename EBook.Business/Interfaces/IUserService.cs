using EBook.Business.ViewModel;

using Microsoft.AspNetCore.Identity;

namespace EBook.Business.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
