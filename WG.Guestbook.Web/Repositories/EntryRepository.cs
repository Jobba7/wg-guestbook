using Microsoft.EntityFrameworkCore;
using WG.Guestbook.Web.Domain;
using WG.Guestbook.Web.Infrastructure;

namespace WG.Guestbook.Web.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly GuestbookDbContext _context;
        private readonly ILogger<EntryRepository> _logger;

        public EntryRepository(GuestbookDbContext context, ILogger<EntryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<Entry> GetAll()
        {
            return _context.Entries.Include(e => e.Author).Include(e => e.Likes).OrderByDescending(e => e.CreateDate);
        }

        public async Task<bool> HasEntryOnDateAsync(User user, DateOnly date)
        {
            return await _context.Entries.Include(e => e.Author).Where(e => e.Author == user).AnyAsync(e => e.VisitDate == date);
        }

        public async Task<Entry?> GetByIdAsync(string id)
        {
            return await _context.Entries.Include(e => e.Author).Include(e => e.Likes).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> AddAsync(Entry entry)
        {
            _context.Entries.Add(entry);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Entry entry)
        {
            _context.Entries.Update(entry);
            return await SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(Entry entry)
        {
            _context.Entries.Remove(entry);
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            var succeeded = false;
            try
            {
                succeeded = await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return succeeded;
        }
    }
}
