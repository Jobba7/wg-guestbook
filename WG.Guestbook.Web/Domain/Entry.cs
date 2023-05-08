namespace WG.Guestbook.Web.Domain
{
    public class Entry
    {
        public string Id { get; set; }

        public string Content { get; set; } = default!;

        public DateOnly VisitDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public User Author { get; set; } = default!;

        public ICollection<User> Likes { get; set; } = default!;

        public Entry()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
