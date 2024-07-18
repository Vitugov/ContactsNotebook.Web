using AuthenticationServer.Api.DataAccess;
using AuthenticationServer.Api.Models.Identity;
using AuthenticationServer.Api.Services.IdentityRepository;
using AuthenticationServer.Api.Services.TokenGenerator;
using ContactsNotebook.Lib.Configuration;
using ContactsNotebook.Lib.Services.JwtTokenHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AuthenticationServer.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var tokenConfiguration = new AccessTokenConfiguration();
            builder.Configuration.Bind("AccessTokenConfiguration", tokenConfiguration);
            builder.Services.AddSingleton(tokenConfiguration);

            var jwtTokenHandler = new JwtTokenHandler(tokenConfiguration);
            builder.Services.AddSingleton(jwtTokenHandler);

            builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
            builder.Services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppIdentityDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, AppIdentityDbContext, Guid>>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = jwtTokenHandler.TokenValidationParameters;
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            await RegisterRoles(app);
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        public static async Task RegisterRoles(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                var roles = new[] { "Administrator", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new ApplicationRole(role));
                    }
                }
            }
        }
    }
}
