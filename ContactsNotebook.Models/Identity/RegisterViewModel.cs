using System.ComponentModel.DataAnnotations;

namespace ContactsNotebook.Models.Identity
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage =
            "Пароль должен содержать не менее 6 символов, содержать как минимум одну цифру, одну букву в нижнем регистре и одну заглавную букву.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Register as Admin")]
        public bool IsAdmin { get; set; }
    }
}
