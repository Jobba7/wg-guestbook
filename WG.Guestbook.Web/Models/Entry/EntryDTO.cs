namespace WG.Guestbook.Web.Models.Entry
{
    public class EntryDTO
    {
        public string EntryId { get; set; } = default!;

        public string AuthorName { get; set; } = default!;

        public string VisitDate { get; set; } = default!;

        public string ContentPreview { get; set; } = default!;

        public int NumberOfLikes { get; set; }

        public bool IsLiked { get; set; }
    }
}
