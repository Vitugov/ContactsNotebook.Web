namespace ContactsNotebook.Models.Identity
{
    public class TokenResponse(string accessToken)
    {
        public string AccessToken { get; set; } = accessToken;
    }
}
