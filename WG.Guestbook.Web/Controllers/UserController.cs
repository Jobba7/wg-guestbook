using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
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
                    RoleNames = string.Join(", ", roles)
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

        [HttpGet]

        public async Task<IActionResult> UpdateUserRoles(string? id)
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

            //var userRoleNames = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            var roleList = new List<RoleDTO>();
            foreach (var role in roles)
            {
                var roleName = role.Name;
                if (!string.IsNullOrEmpty(roleName))
                {
                    //var isInRole = userRoleNames.Contains(roleName, StringComparer.OrdinalIgnoreCase);
                    var isInRole = await _userManager.IsInRoleAsync(user, roleName);
                    roleList.Add(new RoleDTO
                    {
                        Id = role.Id,
                        Name = roleName,
                        Selected = isInRole
                    });
                }
            }

            var model = new UpdateUserRolesViewModel
            {
                Roles = roleList,
                UserId = user.Id,
                UserName = user.UserName
            };
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateUserRoles(UpdateUserRolesViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if(user == null)
            {
                return NotFound();
            }

            foreach (var role in model.Roles)
            {
                var roleName = role.Name;
                var roleSelected = role.Selected;
                var isInRole = await _userManager.IsInRoleAsync(user, roleName);

                if (roleSelected && !isInRole)
                {
                    var result = await _userManager.AddToRoleAsync(user, roleName);
                    _logger.LogInformation($"Add user {user.UserName} to role {roleName}: {result}");
                }
                else if (!roleSelected && isInRole)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                    _logger.LogInformation($"Remove user {user.UserName} from role {roleName}: {result}");
                }
                else
                {
                    // Role is already correctly assigned to the user
                    continue;
                }
            }

            return RedirectToAction("Index");
        }
    }
}
