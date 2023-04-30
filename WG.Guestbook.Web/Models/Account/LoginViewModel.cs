using System.ComponentModel.DataAnnotations;

namespace WG.Guestbook.Web.Models.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Nutzername")]
        [Required(ErrorMessage = "Bitte gib deinen Nutzernamen ein.")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Passwort")]
        [Required(ErrorMessage = "Bitte gib dein Passwort ein.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}
