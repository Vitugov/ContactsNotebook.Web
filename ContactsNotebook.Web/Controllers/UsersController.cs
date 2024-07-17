using ContactsNotebook.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNotebook.Web.Controllers
{
    [Route("/[controller]/[action]")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll() => Json(new { data = await LoadFromIdentities([.. _userManager.Users]) });

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {
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

        private async Task<List<UserView>> LoadFromIdentities(IEnumerable<IdentityUser> identityUsers)
        {
            var usersList = new List<UserView>();
            //foreach (var identityUser in identityUsers)
            //{
            //    var isAdmin = await _userManager.IsInRoleAsync(identityUser, "Administrator");
            //    var newUser = new UserView { Id = identityUser.Id, Email = identityUser.Email, IsAdmin = isAdmin };
            //    usersList.Add(newUser);
            //}
            return usersList;
        }
    }
}
