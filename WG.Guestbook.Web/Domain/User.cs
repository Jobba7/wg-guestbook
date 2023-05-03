using Microsoft.AspNetCore.Identity;

namespace WG.Guestbook.Web.Domain
{
    public class User : IdentityUser
    {
        public ICollection<Entry> Entries { get; set; } = default!;

        public User(string userName) : base(userName) { }
    }
}
