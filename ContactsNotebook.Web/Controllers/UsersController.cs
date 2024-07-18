using ContactsNotebook.Lib.Models.Identity;
using ContactsNotebook.Lib.Services.ApiClients.Authentication;
using ContactsNotebook.Lib.Services.JwtTokenHandler;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNotebook.Web.Controllers
{
    [Route("/[controller]/[action]")]
    public class UsersController(IAuthenticationApiClient authenticationApiClient, JwtTokenHandler jwtTokenHandler) : Controller
    {
        private readonly IAuthenticationApiClient _authenticationApiClient = authenticationApiClient;
        private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;
        public IActionResult Index()
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jsonString = await _authenticationApiClient.GetUsersAsync();
            return Content(jsonString, "application/json");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var result = await _authenticationApiClient.RegisterUserAsync(model);
            if (!result)
            {
                return BadRequest();
            }
            return RedirectToAction("Index","Users");
        }

        [HttpDelete("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            if (!Guid.TryParse(id, out var guid))
            { 
                return BadRequest();
            }
            var success = await _authenticationApiClient.DeleteUserAsync(guid);
            if (!success)
            {
                return StatusCode(500, new { ErrorMessage = "При удалении пользователя возникла ошибка." });
            }
            return Ok();
        }
    }
}
