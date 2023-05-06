using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.User;
using WG.Guestbook.Web.Services;

namespace WG.Guestbook.Web.Components
{
    public class UserList : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UserList(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allUsers = await _userService.GetAll();
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Admin");

            var model = new UserListViewModel
            {
                Users = allUsers,
                IsAdmin = isAdmin
            };
            return View(model);
        }
    }
}
