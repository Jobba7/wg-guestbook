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

            if (!result.Succeeded)
            {
                return result;
            }

            await AddToRoleGuestAsync(user);
            await SignInAsync(user);

            return result;
        }

        private async Task<IdentityResult> AddToRoleGuestAsync(User user)
        {
            const string roleName = "Guest";
            var result = await _userManager.AddToRoleAsync(user, roleName);

            _logger.LogInformation($"Add user {user.UserName} to role {roleName}: {result}");

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

        public async Task<IdentityResult> UpdateAccountAsync(UpdateAccountViewModel model)
        {
            var currentUserName = model.CurrentUserName.Trim();
            var user = await _userManager.FindByNameAsync(currentUserName);

            if (user == null)
            {
                _logger.LogWarning($"User {currentUserName} not found.");

                var error = new IdentityError()
                {
                    Code = "UserNotFound",
                    Description = $"User '{currentUserName}' could not be found."
                };

                return IdentityResult.Failed(error);
            }

            // Change Password
            var result = IdentityResult.Success;
            var oldPassword = model.CurrentPassword?.Trim();
            var newPassword = model.NewPassword?.Trim();

            if (!string.IsNullOrEmpty(oldPassword) &&
                !string.IsNullOrEmpty(newPassword))
            {
                result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

                _logger.LogInformation($"Change Password from {currentUserName}: {result}");

                if (!result.Succeeded)
                {
                    return result;
                }
            }

            // Change Username
            var newUserName = model.NewUserName.Trim();

            if (!newUserName.Equals(currentUserName))
            {
                user.UserName = newUserName;
                result = await _userManager.UpdateAsync(user);

                _logger.LogInformation($"Update username from {currentUserName} to {newUserName}: {result}");

                if (!result.Succeeded)
                {
                    return result;
                }

                if (!await SignInAsync(user))
                {
                    _logger.LogWarning($"Sign in user {user.UserName} (old name {currentUserName}) failed!");
                }
            }

            return result;
        }
    }
}
