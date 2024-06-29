using ContactsNotebook.DataAccess;
using Microsoft.EntityFrameworkCore;
namespace ContactsNotebook.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllersWithViews();
            var app = builder.Build();
            
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
