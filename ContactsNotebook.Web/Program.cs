using AuthenticationServer.ApiClient;
using AuthenticationServer.Client;
using ContactsNotebook.ApiClient;
using ContactsNotebook.Middlewares;
using Microsoft.EntityFrameworkCore;
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

            // Регистрация IHttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            // Регистрация AuthorizationHandler
            builder.Services.AddTransient<AuthorizationHandler>();

            builder.Services.AddSingleton<IContactsApiClient, ContactsApiClient>();
            builder.Services.AddSingleton<IAuthenticationApiClient, AuthenticationApiClient>();

            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRequestHiddenPropertiesSupport();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }


    }
}
