using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
builder.Services.AddDbContext<GuestbookDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<GuestbookDbContext>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
