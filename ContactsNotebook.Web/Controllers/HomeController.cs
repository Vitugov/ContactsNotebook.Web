using ContactsNotebook.Lib.Models;
using ContactsNotebook.Lib.Services.ApiClients.Contacts;
using ContactsNotebook.Lib.Services.JwtTokenHandler;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNotebook.Web.Controllers
{
    public class HomeController(IContactsApiClient contactsApiClient, JwtTokenHandler jwtTokenHandler) : Controller
    {
        private readonly IContactsApiClient _contactsApiClient = contactsApiClient;
        private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;

        [HttpGet("/")]
        public IActionResult Contacts()
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);

            return View();
        }

        [HttpGet("/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var jsonString = await _contactsApiClient.GetContactsAsync();
            return Content(jsonString, "application/json");
        }

        [HttpGet("/{id:int}")]
        public async Task<IActionResult> Display(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var contact = await _contactsApiClient.GetContactByIdAsync((int)id);
            if (contact == null)
            {
                return NotFound();
            }

            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);

            return View(contact);
        }

        [HttpDelete("/Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactsApiClient.DeleteContactAsync((int)id);
            if (result == true)
            {
                return Json(new { success = true, message = "Удаление прошло успешно" });
            }
            return Json(new { success = false, message = "При удалении возникла ошибка" });
        }

        [HttpGet("/Edit")]
        public IActionResult Edit()
        {
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            ViewBag.RequestMethod = "POST";
            return View();
        }

        [HttpPost("/Edit")]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactsApiClient.CreateContactAsync(contact);
                if (result)
                {
                    return RedirectToAction("Contacts");
                }
                return StatusCode(500, new { success = false, message = "При создании объекта возникла ошибка" });
            }
            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            ViewBag.RequestMethod = "POST";
            return View(contact);
        }

        [HttpGet("/Edit/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var contact = await _contactsApiClient.GetContactByIdAsync((int)id);
            if (contact == null)
            {
                return NotFound();
            }

            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            ViewBag.RequestMethod = "PUT";
            return View(contact);
        }

        [HttpPut("/Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            contact.Id = id;
            if (id == 0)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var result = await _contactsApiClient.UpdateContactAsync(contact);
                if (!result)
                {
                    return StatusCode(500, new { success = false, message = "При обновлении объекта возникла ошибка" });
                }
                return RedirectToAction("Contacts");
            }

            ViewBag.UserRole = jwtTokenHandler.GetRoleFromCookieToken(ControllerContext);
            ViewBag.RequestMethod = "PUT";
            return View(contact);
        }
    }
}
