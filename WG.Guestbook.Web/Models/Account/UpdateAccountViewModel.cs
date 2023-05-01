using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WG.Guestbook.Web.Models.Account
{
    public class UpdateAccountViewModel
    {
        [Required]
        public string CurrentUserName { get; set; } = string.Empty;

        [Display(Name = "Nutzername")]
        [Required(ErrorMessage = "Bitte gib einen Nutzernamen an.")]
        public string NewUserName { get; set; } = string.Empty;

        [Display(Name = "Altes Passwort")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Display(Name = "Neues Passwort")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Display(Name = "Neues Passwort bestätigen")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwörter stimmen nicht überein.")]
        public string? ConfirmNewPassword { get; set; }
    }
}
