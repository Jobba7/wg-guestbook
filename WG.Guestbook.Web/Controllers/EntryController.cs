using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WG.Guestbook.Web.Models.Entry;
using WG.Guestbook.Web.Services;

namespace WG.Guestbook.Web.Controllers
{
    [Authorize]
    public class EntryController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly ILogger<EntryController> _logger;

        private readonly DateOnly dateMin = new(2023, 1, 1);
        private readonly DateOnly dateMax = DateOnly.FromDateTime(DateTime.Now);

        public EntryController(IEntryService entryService, ILogger<EntryController> logger)
        {
            _entryService = entryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entries = await _entryService.GetAllAsync();
            return View(entries);
        }

        [HttpGet]
        public new IActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Guest")]
        public IActionResult Create()
        {
            var model = new CreateEntryViewModel()
            {
                DateMax = dateMax.ToString("yyyy-MM-dd"),
                DateMin = dateMin.ToString("yyyy-MM-dd"),
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> Create(CreateEntryViewModel model)
        {
            model.DateMax = dateMax.ToString("yyyy-MM-dd");
            model.DateMin = dateMin.ToString("yyyy-MM-dd");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var author = await _entryService.GetUserAsync(User);
            if (author == null)
            {
                _logger.LogError($"No current user found. Username: {User.Identity?.Name}");
                return BadRequest();
            }

            var result = await _entryService.CreateAsync(author, model);

            if (!result.Succeeded)
            {
                var justOneError = result.Errors.Count() == 1;

                foreach (var error in result.Errors)
                {
                    if (error.Code.Contains("date", StringComparison.OrdinalIgnoreCase) && justOneError)
                    {
                        ModelState.AddModelError(nameof(model.VisitDate), error.Description);
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
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("NotFound");
            }

            var entry = await _entryService.GetByIdAsync(id);

            if (entry == null)
            {
                _logger.LogWarning($"Entry with id {id} could not be found.");
                return RedirectToAction("NotFound");
            }

            var canEdit = false;
            var canDelete = false;
            var user = await _entryService.GetUserAsync(User);
            if (user != null)
            {
                canEdit = _entryService.UserCanEditEntry(user, entry);
                canDelete = await _entryService.UserCanDeleteEntryAsync(user, entry);
            }

            var model = new EntryDetailsViewModel()
            {
                EntryId = entry.Id,
                Content = entry.Content,
                VisitDate = entry.VisitDate,
                CreateDate = entry.CreateDate,
                AuthorName = entry.Author.UserName,
                AuthorId = entry.Author.Id,
                CanEdit = canEdit,
                CanDelete = canDelete
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("NotFound");
            }

            var entry = await _entryService.GetByIdAsync(id);
            if (entry == null)
            {
                _logger.LogWarning($"Entry with id {id} could not be found.");
                return RedirectToAction("NotFound");
            }

            var user = await _entryService.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning($"User {User.Identity?.Name} could not be found.");
                return BadRequest();
            }

            var canEdit = _entryService.UserCanEditEntry(user, entry);
            if (!canEdit)
            {
                _logger.LogWarning($"User {user.UserName} tries to edit entry by {entry.Author.UserName} (does not have permission to do so).");
                return Forbid();
            }

            var model = new EditEntryViewModel()
            {
                EntryId = entry.Id,
                AuthorId = entry.Author.Id,
                Content = entry.Content,
                VisitDate = entry.VisitDate,
                DateMax = dateMax.ToString("yyyy-MM-dd"),
                DateMin = dateMin.ToString("yyyy-MM-dd")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEntryViewModel model)
        {
            model.DateMax = dateMax.ToString("yyyy-MM-dd");
            model.DateMin = dateMin.ToString("yyyy-MM-dd");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = model.EntryId;

            var entry = await _entryService.GetByIdAsync(id);
            if (entry == null)
            {
                _logger.LogWarning($"Entry with id {id} could not be found.");
                return RedirectToAction("NotFound");
            }

            var user = await _entryService.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning($"User {User.Identity?.Name} could not be found.");
                return BadRequest();
            }

            var result = await _entryService.UpdateAsync(user, entry, model);

            if (!result.Succeeded)
            {
                var justOneError = result.Errors.Count() == 1;

                foreach (var error in result.Errors)
                {
                    if (error.Code.Contains("date", StringComparison.OrdinalIgnoreCase) && justOneError)
                    {
                        ModelState.AddModelError(nameof(model.VisitDate), error.Description);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(model);
            }

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("NotFound");
            }

            var entry = await _entryService.GetByIdAsync(id);
            if (entry == null)
            {
                _logger.LogWarning($"Entry with id {id} could not be found.");
                return RedirectToAction("NotFound");
            }

            var user = await _entryService.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning($"User {User.Identity?.Name} could not be found.");
                return BadRequest();
            }

            var canDelete = await _entryService.UserCanDeleteEntryAsync(user, entry);
            if (!canDelete)
            {
                _logger.LogWarning($"User {user.UserName} tried to delete entry by {entry.Author.UserName} (does not have permission to do so).");
                return Forbid();
            }

            await _entryService.DeleteAsync(user, entry);

            return RedirectToAction("Index");
        }
    }
}
