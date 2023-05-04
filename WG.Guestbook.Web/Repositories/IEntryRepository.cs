using WG.Guestbook.Web.Domain;

namespace WG.Guestbook.Web.Repositories
{
    public interface IEntryRepository
    {
        IQueryable<Entry> GetAll();

        Task<bool> HasEntryOnDateAsync(User user, DateOnly date);

        Task<Entry?> GetByIdAsync(string id);

        Task<bool> AddAsync(Entry entry);

        Task<bool> UpdateAsync(Entry entry);

        Task<bool> RemoveAsync(Entry entry);
    }
}
