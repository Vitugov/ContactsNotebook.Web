using System.ComponentModel.DataAnnotations;

namespace ContactsNotebook.Lib.Models.Identity
{
    public class LoginViewModel : ILoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
