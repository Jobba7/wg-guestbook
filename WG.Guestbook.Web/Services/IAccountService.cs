using Microsoft.AspNetCore.Identity;
using WG.Guestbook.Web.Models.Account;

namespace WG.Guestbook.Web.Services
{
    public interface IAccountService
    {
        bool CheckRegistrationCode(string? code);

        Task<IdentityResult> RegisterAsync(RegisterViewModel model);

        Task<SignInResult> LoginAsync(LoginViewModel model, bool isPersistent = true, bool lockoutOnFailure = true);

        Task<bool> LogoutAsync(string? userName = null);

        Task<IdentityResult> UpdateAccountAsync(UpdateAccountViewModel model);
    }
}
