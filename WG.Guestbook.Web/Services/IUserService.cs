using Microsoft.AspNetCore.Identity;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.User;

namespace WG.Guestbook.Web.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();

        Task<User?> GetById(string id);

        Task<IdentityResult> DeleteAsync(User user);

        Task<List<RoleDTO>> GetAllRolesSelectedByUserAsync(User user);

        Task<bool> UpdateUserRoles(UpdateUserRolesViewModel model);
    }
}
