using ContactsNotebook.Lib.Models.Identity;
using ContactsNotebook.Lib.Services.ApiClients.Authentication;
using ContactsNotebook.Lib.Services.JwtTokenHandler;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactsNotebook.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController(IAuthenticationApiClient authenticationApiClient, JwtTokenHandler jwtTokenHandler) : Controller
    {
        private readonly IAuthenticationApiClient _authenticationApiClient = authenticationApiClient;
        private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;


        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
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

            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
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
