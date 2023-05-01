using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WG.Guestbook.Web.Models.Account;
using WG.Guestbook.Web.Services.Account;

namespace WG.Guestbook.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var userName = User.Identity?.Name;

            if (userName == null)
            {
                return BadRequest();
            }

            var model = new UpdateAccountViewModel()
            {
                CurrentUserName = userName,
                NewUserName = userName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.CurrentPassword) &&
                string.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError(nameof(model.NewPassword), "Bitte gib dein neues Passwort an.");

                return View(model);
            }

            var result = await _accountService.UpdateAccountAsync(model);

            if (!result.Succeeded)
            {
                var justOneError = result.Errors.Count() == 1;

                foreach (var error in result.Errors)
                {
                    if (error.Code.Contains("user", StringComparison.OrdinalIgnoreCase) && justOneError)
                    {
                        ModelState.AddModelError(nameof(model.NewUserName), error.Description);
                    }
                    else if (error.Code.Contains("password", StringComparison.OrdinalIgnoreCase) && justOneError)
                    {
                        ModelState.AddModelError(nameof(model.ConfirmNewPassword), error.Description);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(model);
            }

            return RedirectToAction("Index");
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

            var result = await _accountService.RegisterAsync(model);

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

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            var model = new LoginViewModel() { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.LoginAsync(model);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(model.Password), "Nutzname oder Passwort falsch.");

                return View(model);
            }

            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity?.Name;

            await _accountService.LogoutAsync(userName);

            return RedirectToAction("Index", "Home");
        }
    }
}
