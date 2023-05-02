using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.User;

namespace WG.Guestbook.Web.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var allUsers = new List<UserDTO>();
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
                allUsers.Add(userDTO);
            }

            return allUsers;
        }

        public async Task<User?> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<List<RoleDTO>> GetAllRolesSelectedByUserAsync(User user)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var allRoles = new List<RoleDTO>();

            foreach (var role in roles)
            {
                var roleName = role.Name;
                if (!string.IsNullOrEmpty(roleName))
                {
                    var isInRole = await _userManager.IsInRoleAsync(user, roleName);
                    allRoles.Add(new RoleDTO
                    {
                        Id = role.Id,
                        Name = roleName,
                        Selected = isInRole
                    });
                }
            }

            return allRoles;
        }

        public async Task<bool> UpdateUserRoles(UpdateUserRolesViewModel model)
        {
            var userId = model.UserId.Trim();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
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

            return true;
        }
    }
}
