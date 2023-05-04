using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.Entry;
using WG.Guestbook.Web.Repositories;

namespace WG.Guestbook.Web.Services
{
    public class EntryService : IEntryService
    {
        private readonly IEntryRepository _entryRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EntryService> _logger;

        public EntryService(IEntryRepository entryRepository, UserManager<User> userManager, ILogger<EntryService> logger)
        {
            _entryRepository = entryRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IEnumerable<Entry>> GetAllAsync()
        {
            return await _entryRepository.GetAll().ToListAsync();
        }

        public async Task<Entry?> GetByIdAsync(string id)
        {
            return await _entryRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public async Task<Result> CreateAsync(User user, CreateEntryViewModel model)
        {
            var visitDate = model.VisitDate;
            var result = await CheckDateAsync(user, visitDate);
            if (!result.Succeeded)
            {
                return result;
            }

            var content = model.Content.Trim();
            var createDate = DateTime.Now;
            var entry = new Entry()
            {
                Content = content,
                VisitDate = visitDate,
                CreateDate = createDate,
                Author = user,
            };

            var succeeded = await _entryRepository.AddAsync(entry);

            if (!succeeded)
            {
                _logger.LogError($"Create Entry from {user.UserName} failed.");

                return Result.Failed(new Error
                {
                    Code = "AddError",
                    Description = "Es ist ein Fehler aufgetreten. Bitte versuche es erneut."
                });
            }

            _logger.LogInformation($"Create Entry for {user.UserName} succeeded.");

            return Result.Success;
        }

        public async Task<Result> UpdateAsync(User user,Entry entry, EditEntryViewModel model)
        {
            if (!UserIsAuthor(user,entry))
            {
                _logger.LogWarning($"User {user.UserName} tried to edit entry by {entry.Author.UserName} (does not have permission to do so).");

                return Result.Failed(new Error
                {
                    Code = "NoPermission",
                    Description = "Nutzer ist nicht der Autor."
                });
            }

            var newVisitDate = model.VisitDate;
            var currentVisitDate = entry.VisitDate;
            if (newVisitDate != currentVisitDate)
            {
                var result = await CheckDateAsync(user, newVisitDate);
                if (!result.Succeeded)
                {
                    return result;
                }

                entry.VisitDate = newVisitDate;
            }

            entry.Content = model.Content.Trim();

            var succeeded = await _entryRepository.UpdateAsync(entry);

            _logger.LogInformation($"User {user.UserName} edited entry by {entry.Author.UserName}: {succeeded}");

            return Result.Success;
        }

        public async Task<Result> DeleteAsync(User user ,Entry entry)
        {
            var canDelete = await UserCanDeleteEntryAsync(user, entry);
            if (!canDelete)
            {
                _logger.LogWarning($"User {user.UserName} tried to delete entry by {entry.Author.UserName} (does not have permission to do so).");

                return Result.Failed(new Error
                {
                    Code = "NoPermission",
                    Description = "Nutzer ist nicht der Autor oder ein Admin."
                });
            }

            var succeeded = await _entryRepository.RemoveAsync(entry);

            _logger.LogInformation($"User {user.UserName} deletes entry by {entry.Author.UserName}: {succeeded}");

            return Result.Success;
        }

        public async Task<bool> UserCanDeleteEntryAsync(User user, Entry entry)
        {
            return UserIsAuthor(user, entry) || await _userManager.IsInRoleAsync(user, "Admin");
        }

        public bool UserCanEditEntry(User user, Entry entry)
        {
            return UserIsAuthor(user, entry);
        }

        private static bool UserIsAuthor(User user, Entry entry)
        {
            return entry.Author.Id == user.Id;
        }

        private async Task<Result> CheckDateAsync(User user, DateOnly date)
        {
            var result = Result.Success;

            var dateMin = new DateOnly(2023, 1, 1);
            var dateMax = DateOnly.FromDateTime(DateTime.Now);

            if (date < dateMin)
            {
                return Result.Failed(new Error
                {
                    Code = "MinDate",
                    Description = $"Es sind nur Eintrag nach dem {dateMin} erlaubt."
                });
            }

            if (date > dateMax)
            {
                return Result.Failed(new Error
                {
                    Code = "MaxDate",
                    Description = $"Wähle bitte einen Tag der nicht in der Zukunft liegt."
                });
            }

            var hasEntryOnDate = await _entryRepository.HasEntryOnDateAsync(user, date);
            if (hasEntryOnDate)
            {
                return Result.Failed(new Error
                {
                    Code = "HasEntryOnDate",
                    Description = "Du hast schon einen Eintrag an diesem Tag. Wähle ein anderes Datum aus."
                });
            }

            return result;
        }
    }
}
