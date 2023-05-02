using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.User;

namespace WG.Guestbook.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var usersDTO = new List<UserDTO>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDTO = new UserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    RoleNames = string.Join(",", roles)
                };
                usersDTO.Add(userDTO);
            }

            var model = new UserListViewModel() { Users = usersDTO };

            return View(model);
        }
    }
}
