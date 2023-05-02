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
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<User> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            _logger.LogInformation($"Delete user {user.UserName}: {result}");

            return RedirectToAction("Index");
        }
    }
}
