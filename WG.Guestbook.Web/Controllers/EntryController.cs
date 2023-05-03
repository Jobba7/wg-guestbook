using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Infrastructure;
using WG.Guestbook.Web.Models.Entry;

namespace WG.Guestbook.Web.Controllers
{
    [Authorize]
    public class EntryController : Controller
    {
        private readonly GuestbookDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EntryController> _logger;

        private readonly DateOnly dateMin = new(2023, 1, 1);
        private readonly DateOnly dateMax = DateOnly.FromDateTime(DateTime.Now);

        public EntryController(GuestbookDbContext context, UserManager<User> userManager, ILogger<EntryController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entries = await _context.Entries.Include(e => e.Author).OrderByDescending(e => e.CreateDate).ToListAsync();

            return View(entries);
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

            var author = await _userManager.GetUserAsync(User);
            if (author == null)
            {
                _logger.LogError($"No current user found. Username: {User.Identity?.Name}");
                return BadRequest();
            }

            var visitDate = model.VisitDate;
            if (visitDate < dateMin)
            {
                ModelState.AddModelError(nameof(model.VisitDate), $"Es sind nur Eintrag nach dem {dateMin} erlaubt.");
                return View(model);
            }
            if (visitDate > dateMax)
            {
                ModelState.AddModelError(nameof(model.VisitDate), $"Wähle bitte einen Tag der nicht in der Zukunft liegt.");
                return View(model);
            }

            var hasEntryOnDate = await _context.Entries.Include(e => e.Author).Where(e => e.Author == author).AnyAsync(e => e.VisitDate == visitDate);
            if (hasEntryOnDate)
            {
                ModelState.AddModelError(nameof(model.VisitDate), "Du hast schon einen Eintrag an diesem Tag. Wähle ein anderes Datum aus.");
                return View(model);
            }

            var content = model.Content.Trim();
            var createDate = DateTime.Now;
            var entry = new Entry()
            {
                Content = content,
                VisitDate = visitDate,
                CreateDate = createDate,
                Author = author,
            };

            _context.Entries.Add(entry);
            var succeeded = await _context.SaveChangesAsync() > 0;

            if (!succeeded)
            {
                _logger.LogError($"Create Entry from {author.UserName} failed.");
                ModelState.AddModelError(string.Empty, "Es ist ein Fehler aufgetreten. Bitte versuche es erneut.");
                return View(model);
            }

            _logger.LogInformation($"Create Entry for {author.UserName} succeeded.");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(id);
            }

            var entry = await _context.Entries.Include(e => e.Author).FirstOrDefaultAsync(e => e.Id == id);

            if (entry == null)
            {
                _logger.LogWarning($"Entry with id {id} could not be found.");
                return NotFound(id);
            }

            var canEdit = false;
            var canDelete = false;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var isAuthor = entry.Author.Id == user.Id;
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                canEdit = isAuthor;
                canDelete = isAuthor || isAdmin;
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
                return NotFound(id);
            }

            var entry = await _context.Entries.Include(e => e.Author).FirstOrDefaultAsync(e => e.Id == id);
            if (entry == null)
            {
                _logger.LogWarning($"Entry with id {id} could not be found.");
                return NotFound(id);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning($"User {User.Identity?.Name} could not be found.");
                return BadRequest();
            }

            var isAuthor = entry.Author.Id == user.Id;
            if (!isAuthor)
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = model.EntryId;

            var entry = await _context.Entries.Include(e => e.Author).FirstOrDefaultAsync(e => e.Id == id);
            if (entry == null)
            {
                _logger.LogWarning($"Entry with id {id} could not be found.");
                return NotFound(id);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning($"User {User.Identity?.Name} could not be found.");
                return BadRequest();
            }

            var isAuthor = entry.Author.Id == user.Id;
            if (!isAuthor)
            {
                _logger.LogWarning($"User {user.UserName} tried to edit entry by {entry.Author.UserName} (does not have permission to do so).");
                return Forbid();
            }

            var newVisitDate = model.VisitDate;
            var currentVisitDate = entry.VisitDate;
            if (newVisitDate != currentVisitDate)
            {
                if (newVisitDate < dateMin)
                {
                    ModelState.AddModelError(nameof(model.VisitDate), $"Es sind nur Eintrag nach dem {dateMin} erlaubt.");
                    return View(model);
                }
                if (newVisitDate > dateMax)
                {
                    ModelState.AddModelError(nameof(model.VisitDate), $"Wähle bitte einen Tag der nicht in der Zukunft liegt.");
                    return View(model);
                }

                var hasEntryOnDate = await _context.Entries.Include(e => e.Author).Where(e => e.Author == user).AnyAsync(e => e.VisitDate == newVisitDate);
                if (hasEntryOnDate)
                {
                    ModelState.AddModelError(nameof(model.VisitDate), "Du hast schon einen Eintrag an diesem Tag. Wähle ein anderes Datum aus.");
                    return View(model);
                }

                entry.VisitDate = newVisitDate;
            }

            entry.Content = model.Content.Trim();

            _context.Entries.Update(entry);
            var succeeded = await _context.SaveChangesAsync() > 0;

            _logger.LogInformation($"User {user.UserName} edited entry by {entry.Author.UserName}: {succeeded}");

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(id);
            }

            var entry = await _context.Entries.Include(e => e.Author).FirstOrDefaultAsync(e => e.Id == id);
            if(entry == null)
            {
                _logger.LogWarning($"Entry with id {id} could not be found.");
                return NotFound(id);
            }

            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                _logger.LogWarning($"User {User.Identity?.Name} could not be found.");
                return BadRequest();
            }

            var isAuthor = entry.Author.Id == user.Id;
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if(!isAuthor && !isAdmin)
            {
                _logger.LogWarning($"User {user.UserName} tried to delete entry by {entry.Author.UserName} (does not have permission to do so).");
                return Forbid();
            }

            _context.Entries.Remove(entry);
            var succeeded = await _context.SaveChangesAsync() > 0;

            _logger.LogInformation($"User {user.UserName} deletes entry by {entry.Author.UserName}: {succeeded}");

            return RedirectToAction("Index");
        }
    }
}
