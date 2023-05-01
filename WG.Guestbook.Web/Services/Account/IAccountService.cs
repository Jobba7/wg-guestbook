using Microsoft.AspNetCore.Identity;
using WG.Guestbook.Web.Models.Account;

namespace WG.Guestbook.Web.Services.Account
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);

        Task<SignInResult> LoginAsync(LoginViewModel model, bool isPersistent = true, bool lockoutOnFailure = false);

        Task<bool> LogoutAsync(string? userName = null);

        Task<IdentityResult> UpdateAccountAsync(UpdateAccountViewModel model);
    }
}
