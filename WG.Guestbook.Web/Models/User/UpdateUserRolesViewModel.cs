namespace WG.Guestbook.Web.Models.User
{
    public class UpdateUserRolesViewModel
    {
        public string UserId { get; set; } = default!;
        public string? UserName { get; set; }
        public List<RoleDTO> Roles { get; set; } = default!;
    }
}
