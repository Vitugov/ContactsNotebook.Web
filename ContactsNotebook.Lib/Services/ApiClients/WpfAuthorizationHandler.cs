using ContactsNotebook.Lib.Models.Identity;

namespace ContactsNotebook.Lib.Services.ApiClients
{
    public class WpfAuthorizationHandler(AppUser wpfUser) : DelegatingHandler
    {
        private readonly AppUser _wpfUser = wpfUser;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = _wpfUser.AccessToken;

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
