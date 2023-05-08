using System.Security.Claims;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Models.Entry;

namespace WG.Guestbook.Web.Services
{
    public interface IEntryService
    {
        Task<IEnumerable<EntryDTO>> GetAllAsync(User user);

        Task<Entry?> GetByIdAsync(string id);

        Task<User?> GetUserAsync(ClaimsPrincipal user);

        Task<Result> CreateAsync(User user, CreateEntryViewModel model);

        Task<Result> UpdateAsync(User user, Entry entry, EditEntryViewModel model);

        Task<Result> DeleteAsync(User user, Entry entry);

        Task<bool> UserCanDeleteEntryAsync(User user, Entry entry);

        bool UserCanEditEntry(User user, Entry entry);
    }
}
