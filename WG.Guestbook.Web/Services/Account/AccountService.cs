using Microsoft.AspNetCore.Identity;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.Account;

namespace WG.Guestbook.Web.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountService> _logger;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var userName = model.UserName.Trim();
            var password = model.Password.Trim();

            var user = new User(userName);

            var result = await _userManager.CreateAsync(user, password);

            _logger.LogInformation($"Create user {user.UserName}: {result}");

            if (result.Succeeded)
            {
                await SignInAsync(user);
            }

            return result;
        }

        private async Task<bool> SignInAsync(User user, bool isPersistent = true)
        {
            var task = _signInManager.SignInAsync(user, isPersistent);

            await task;

            var succeeded = task.IsCompletedSuccessfully;

            if (succeeded)
            {
                _logger.LogInformation($"Login for user {user.UserName}: Succeeded");
            }
            else
            {
                _logger.LogInformation($"Login for user {user.UserName}: Failed");
            }

            return succeeded;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model, bool isPersistent = true, bool lockoutOnFailure = false)
        {
            var userName = model.UserName.Trim();
            var password = model.Password.Trim();

            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

            _logger.LogInformation($"Login for user {userName}: {result}");

            return result;
        }

        public async Task<bool> LogoutAsync(string? userName = null)
        {
            var task = _signInManager.SignOutAsync();

            await task;

            var succeeded = task.IsCompletedSuccessfully;

            if (succeeded)
            {
                _logger.LogInformation($"Logout for user {userName}: Succeeded");
            }
            else
            {
                _logger.LogInformation($"Logout for user {userName}: Failed");
            }

            return succeeded;
        }
    }
}
