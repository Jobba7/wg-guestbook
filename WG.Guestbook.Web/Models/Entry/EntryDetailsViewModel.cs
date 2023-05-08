namespace WG.Guestbook.Web.Models.Entry
{
    public class EntryDetailsViewModel
    {
        public string EntryId { get; set; } = default!;
        public string Content { get; set; } = default!;

        public DateOnly VisitDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastEditDate { get; set; }

        public string AuthorName { get; set; } = default!;
        public string AuthorId { get; set; } = default!;

        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public int NumberOfLikes { get; set; }
        public bool IsLiked { get; set; }
    }
}
