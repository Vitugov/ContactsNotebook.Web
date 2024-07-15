using ContactsNotebook.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ContactsNotebook.Middlewares;

namespace ContactsNotebook.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseRequestHiddenPropertiesSupport();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
