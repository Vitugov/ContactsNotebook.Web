namespace ContactsNotebook.Lib.Models.Identity
{
    public interface IAppUser
    {
        string AccessToken { get; set; }
        string Email { get; set; }
        string Role { get; set; }
    }
}