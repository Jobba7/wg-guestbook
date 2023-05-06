using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WG.Guestbook.Web.Models.User;
using WG.Guestbook.Web.Services;

namespace WG.Guestbook.Web.Controllers
{
    [Authorize(Roles = "Admin, Roommate")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.DeleteAsync(user);

            _logger.LogInformation($"Delete user {user.UserName}: {result}");

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRoles(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var allRoles = await _userService.GetAllRolesSelectedByUserAsync(user);

            var model = new UpdateUserRolesViewModel
            {
                Roles = allRoles,
                UserId = user.Id,
                UserName = user.UserName
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRolesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var succeded = await _userService.UpdateUserRoles(model);

            if (!succeded)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction("Index");
        }
    }
}
