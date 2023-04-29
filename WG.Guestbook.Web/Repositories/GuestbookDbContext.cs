using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WG.Guestbook.Web.Domain;

namespace WG.Guestbook.Web.Repositories
{
    public class GuestbookDbContext : IdentityDbContext<User>
    {
        public GuestbookDbContext(DbContextOptions<GuestbookDbContext> options) : base(options) 
        {
            // ensure that DbContext type accepts a DbContextOptions<TContext> object in its constructor and passes it to the base constructor for DbContext
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasData(new User("Admin"));
        }
    }
}
