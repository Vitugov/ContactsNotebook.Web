using ContactsNotebook.Lib.Configuration;
using ContactsNotebook.Lib.Middlewares;
using ContactsNotebook.Lib.Services.ApiClients;
using ContactsNotebook.Lib.Services.ApiClients.Authentication;
using ContactsNotebook.Lib.Services.ApiClients.Contacts;
using ContactsNotebook.Lib.Services.JwtTokenHandler;
namespace ContactsNotebook.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient("ContactsApiClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("ContactsApiConnection")!);

            }).AddHttpMessageHandler<AuthorizationHandler>();

            builder.Services.AddHttpClient("AuthenticationApiClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("AuthenticationApiConnection")!);

            }).AddHttpMessageHandler<AuthorizationHandler>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<AuthorizationHandler>();

            var tokenConfiguration = new AccessTokenConfiguration();
            builder.Configuration.Bind("AccessTokenConfiguration", tokenConfiguration);
            builder.Services.AddSingleton(tokenConfiguration);

            var jwtTokenHandler = new JwtTokenHandler(tokenConfiguration);
            builder.Services.AddSingleton(jwtTokenHandler);

            builder.Services.AddSingleton<IContactsApiClient, ContactsApiClient>();
            builder.Services.AddSingleton<IAuthenticationApiClient, AuthenticationApiClient>();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            app.UseRequestHiddenPropertiesSupport();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }


    }
}
