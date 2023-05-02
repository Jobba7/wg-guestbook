using System.ComponentModel.DataAnnotations;

namespace WG.Guestbook.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "Nutzername")]
        [Required(ErrorMessage = "Bitte gib einen Nutzernamen ein.")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Passwort")]
        [Required(ErrorMessage = "Bitte gib ein Passwort ein.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Bestätige das Passwort")]
        [Required(ErrorMessage = "Bitte bestätige dein Passwort.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwörter stimmen nicht überein.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}
