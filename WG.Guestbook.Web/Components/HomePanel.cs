using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.Home;

namespace WG.Guestbook.Web.Components
{
    public class HomePanel : ViewComponent
    {
        private readonly SignInManager<User> _signInManager;

        public HomePanel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {
            var isSignedIn = _signInManager.IsSignedIn((ClaimsPrincipal)User);
            var model = new HomePanelViewModel { IsSignedIn = isSignedIn };
            return View(model);
        }
    }
}
