using Microsoft.AspNetCore.Identity;

namespace WG.Guestbook.Web.Domain
{
    public class User : IdentityUser
    {
        public User(string userName) : base(userName) { }
    }
}
