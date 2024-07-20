namespace ContactsNotebook.Lib.Models.Identity
{
    public interface ILoginViewModel
    {
        string? Email { get; set; }
        string? Password { get; set; }
        bool RememberMe { get; set; }
    }
}