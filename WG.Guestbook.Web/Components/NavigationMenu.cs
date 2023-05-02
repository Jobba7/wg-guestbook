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

        public NavigationMenu(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {
            var userName = User.Identity?.Name;
            var isSignedIn = false;

            if (User is ClaimsPrincipal principal)
            {
                isSignedIn = _signInManager.IsSignedIn(principal);
            }

            var model = new NavigationMenuViewModel()
            {
                IsSignedIn = isSignedIn,
                UserName = userName
            };

            return View(model);
        }
    }
}
