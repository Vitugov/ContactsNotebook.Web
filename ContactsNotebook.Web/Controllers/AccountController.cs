using AuthenticationServer.ApiClient;
using ContactsNotebook.Models.Identity;
using ContactsNotebook.Web.Services.JwtTokenReader;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactsNotebook.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController(IAuthenticationApiClient authenticationApiClient) : Controller
    {
        private readonly IAuthenticationApiClient _authenticationApiClient = authenticationApiClient;



        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.UserRole = JwtTokenReader.GetRole(Request);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _authenticationApiClient.RegisterUserAsync(model);
                if (!result)
                {
                    return BadRequest();
                }
                return RedirectToAction("Contacts", "Home");
            }

            ViewBag.UserRole = JwtTokenReader.GetRole(Request);
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.UserRole = JwtTokenReader.GetRole(Request);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewBag.UserRole = JwtTokenReader.GetRole(Request);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var tokenResponse = await _authenticationApiClient.LoginUserAsync(model);
            if (tokenResponse == null)
            {
                ModelState.AddModelError(string.Empty, "При входе пользователя возникла ошибка.");
                return View(model);
            }

            var accessToken = tokenResponse.AccessToken;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim("JWT", accessToken)
            };

            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var principal = new ClaimsPrincipal(identity);

            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Сохранение refresh токена в HttpOnly куки
            Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return RedirectToAction("Contacts", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            
            if (Request.Cookies["AccessToken"] != null)
            {
                Response.Cookies.Delete("AccessToken");
            }
            var success = await _authenticationApiClient.LogoutUserAsync();
            if (!success)
            {
                return BadRequest();
            }
            return RedirectToAction("Contacts", "Home");
        }
    }
}
