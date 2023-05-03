using System.ComponentModel.DataAnnotations;

namespace WG.Guestbook.Web.Models.Entry
{
    public class EditEntryViewModel
    {
        [Required]
        public string EntryId { get; set; } = default!;
        [Required]
        public string AuthorId { get; set; } = default!;

        [Display(Name = "Inhalt")]
        [Required(ErrorMessage = "Bitte schreibe etwas über den Tag.")]
        public string Content { get; set; } = default!;

        [Display(Name = "Tag des Besuches")]
        [Required(ErrorMessage = "Bitte wähle den Tag aus, an dem du da warst.")]
        [DataType(DataType.Date, ErrorMessage = "Gib bitte ein gültiges Datum ein.")]
        public DateOnly VisitDate { get; set; }

        public string? DateMax { get; set; }
        public string? DateMin { get; set; }
    }
}
