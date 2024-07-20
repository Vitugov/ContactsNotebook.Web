namespace ContactsNotebook.Lib.Models.Identity
{
    public class AppUser : IAppUser
    {
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
        public string AccessToken { get; set; } = "";
    }
}
