using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.Account;

namespace WG.Guestbook.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User(model.UserName);

            var result = await _userManager.CreateAsync(user, model.Password);

            _logger.LogInformation($"Registration for user {user.UserName}: {result}");

            if (!result.Succeeded)
            {
                var justOneError = result.Errors.Count() == 1;
                foreach (var error in result.Errors)
                {
                    if (error.Code.Contains("user", StringComparison.OrdinalIgnoreCase) && justOneError)
                    {
                        ModelState.AddModelError(nameof(model.UserName), error.Description);
                    }
                    else if (error.Code.Contains("password", StringComparison.OrdinalIgnoreCase) && justOneError)
                    {
                        ModelState.AddModelError(nameof(model.Password), error.Description);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: true);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
