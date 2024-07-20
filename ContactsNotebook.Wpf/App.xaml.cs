using ContactsNotebook.Lib.Models.Identity;
using ContactsNotebook.Lib.Services.ApiClients;
using ContactsNotebook.Lib.Services.ApiClients.Authentication;
using ContactsNotebook.Lib.Services.ApiClients.Contacts;
using ContactsNotebook.Wpf.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace ContactsNotebook.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<AppUser>();
            services.AddTransient<WpfAuthorizationHandler>();
            services.AddSingleton<IContactsApiClient, ContactsApiClient>();
            services.AddSingleton<IAuthenticationApiClient, AuthenticationApiClient>();

            services.AddHttpClient("ContactsApiClient", client =>
            {
                client.BaseAddress = new Uri(configuration.GetConnectionString("ContactsApiConnection")!);

            }).AddHttpMessageHandler<WpfAuthorizationHandler>();

            services.AddHttpClient("AuthenticationApiClient", client =>
            {
                client.BaseAddress = new Uri(configuration.GetConnectionString("AuthenticationApiConnection")!);

            }).AddHttpMessageHandler<WpfAuthorizationHandler>();


            services.AddSingleton<LoginView>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<LoginView>();
            mainWindow.Show();
        }
    }

}
