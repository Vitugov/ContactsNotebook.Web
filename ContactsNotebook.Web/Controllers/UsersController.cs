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
            var jsonString = await _authenticationApiClient.GetUsers();
            return Content(jsonString, "application/json");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                //var result = await _userManager.CreateAsync(user, model.Password!);
                //if (result.Succeeded)
                //{
                //    if (model.IsAdmin)
                //    {
                //        await _userManager.AddToRoleAsync(user, "Administrator");
                //        await _userManager.AddToRoleAsync(user, "User");
                //    }
                //    else
                //    {
                //        await _userManager.AddToRoleAsync(user, "User");
                //    }
                //    return RedirectToAction("Index");
                //}
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }
            return View(model);

        }

        [HttpDelete("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            //var userToDelete = await _userManager.FindByIdAsync(id);
            //if (userToDelete == null)
            //{
            //    return NotFound();
            //}
            //if (User.Identity == userToDelete)
            //{
            //    return BadRequest();
            //}
            //await _userManager.DeleteAsync(userToDelete);
            return RedirectToAction("Index");
        }
    }
}
