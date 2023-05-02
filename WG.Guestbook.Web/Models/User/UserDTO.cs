namespace WG.Guestbook.Web.Models.User
{
    public class UserDTO
    {
        public string Id { get; set; } = default!;

        public string? UserName { get; set; }

        public string? RoleNames { get; set; }
    }
}
