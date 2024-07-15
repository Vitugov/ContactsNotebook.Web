using ContactsNotebook.ApiClient;
using ContactsNotebook.DataAccess;
using ContactsNotebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNotebook.Web.Controllers
{
    public class HomeController(IContactsApiClient contactsApiClient) : Controller
    {
        private readonly IContactsApiClient _contactsApiClient = contactsApiClient;

        [HttpGet("/")]
        public IActionResult Contacts()
        {
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
            return View(contact);
        }

        [Authorize(Roles = "Administrator")]
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

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("/Edit")]
        public IActionResult Edit()
        {
            ViewBag.RequestMethod = "put";
            return View();
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpPut("/Edit")]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactsApiClient.CreateContactAsync(contact);
                if (result)
                {
                    return RedirectToAction("Contacts");
                }
                return StatusCode(500, new { message = "При создании объекта возникла ошибка"});
            }
            ViewBag.Editable = true;
            ViewBag.RequestMethod = "put";
            return View(contact);
        }

        [Authorize(Roles = "Administrator")]
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
            ViewBag.RequestMethod = "post";
            return View(contact);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("/Edit/{id:int}")]
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
                    return StatusCode(500, new { message = "При создании обновлении объекта возникла ошибка" });
                }
                return RedirectToAction("Contacts");
            }
            ViewBag.RequestMethod = "post";
            return View(contact);
        }
    }
}
