using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.Layout;

namespace WG.Guestbook.Web.Components
{
    public class NavigationMenu : ViewComponent
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<NavigationMenu> _logger;

        public NavigationMenu(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<NavigationMenu> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var isSignedIn = _signInManager.IsSignedIn((ClaimsPrincipal)User);

            if (!isSignedIn)
            {
                return View(new NavigationMenuViewModel() { IsSignedIn = false });
            }

            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);

            if (user == null)
            {
                _logger.LogWarning($"User {User.Identity?.Name} is signed in, but the user was not found (probably deleted).");

                var task = _signInManager.SignOutAsync();
                await task;

                _logger.LogInformation($"Logout {User.Identity?.Name}: {task.IsCompletedSuccessfully}");

                return View(new NavigationMenuViewModel() { IsSignedIn = false });
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var model = new NavigationMenuViewModel()
            {
                IsSignedIn = isSignedIn,
                UserName = user.UserName,
                IsAdmin = isAdmin
            };

            return View(model);
        }
    }
}
