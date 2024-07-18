using ContactsNotebook.Api.DataAccess;
using ContactsNotebook.Lib.Configuration;
using ContactsNotebook.Lib.Middlewares;
using ContactsNotebook.Lib.Services.JwtTokenHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace ContactsNotebook.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var tokenConfiguration = new AccessTokenConfiguration();
            builder.Configuration.Bind("AccessTokenConfiguration", tokenConfiguration);
            builder.Services.AddSingleton(tokenConfiguration);

            var jwtTokenHandler = new JwtTokenHandler(tokenConfiguration);
            builder.Services.AddSingleton(jwtTokenHandler);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = jwtTokenHandler.TokenValidationParameters;
                //options.Events = new JwtBearerEvents
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var accessToken = context.Request.Cookies["AccessToken"];
                //        if (!string.IsNullOrEmpty(accessToken))
                //        {
                //            context.Token = accessToken;
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
            });

            var app = builder.Build();

            app.UseRequestHiddenPropertiesSupport();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
