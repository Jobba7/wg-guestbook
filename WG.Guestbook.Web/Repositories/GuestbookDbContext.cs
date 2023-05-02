﻿using Microsoft.AspNetCore.Identity;
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

            // seed admin user with admin role
            var role = new IdentityRole("Admin");
            var user = new User("Admin");
            var userRole = new IdentityUserRole<string>()
            {
                RoleId = role.Id,
                UserId = user.Id
            };

            role.NormalizedName = role.Name!.ToUpper();
            user.NormalizedUserName = user.UserName!.ToUpper();
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "123");

            builder.Entity<IdentityRole>().HasData(role);
            builder.Entity<User>().HasData(user);
            builder.Entity<IdentityUserRole<string>>().HasData(userRole);

            // seed roles
            builder.Entity<IdentityRole>().HasData(new IdentityRole[] {
                new IdentityRole()
                {
                    Name = "Guest",
                    NormalizedName = "GUEST"
                },
                new IdentityRole()
                {
                    Name ="Roommate",
                    NormalizedName="ROOMMATE"
                }
            });
        }
    }
}
