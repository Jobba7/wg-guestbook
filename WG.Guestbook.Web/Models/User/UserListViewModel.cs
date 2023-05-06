namespace WG.Guestbook.Web.Models.User
{
    public class UserListViewModel
    {
        public List<UserDTO> Users { get; set; } = default!;

        public bool IsAdmin { get; set; }
    }
}
