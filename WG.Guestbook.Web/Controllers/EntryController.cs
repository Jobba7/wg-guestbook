﻿using Microsoft.AspNetCore.Authorization;
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

            return RedirectToAction("Index");
        }
    }
}
