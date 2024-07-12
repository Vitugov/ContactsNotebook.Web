using ContactsNotebook.DataAccess;
using ContactsNotebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNotebook.Web.Controllers
{
    public class HomeController(ApplicationDbContext dbContext) : Controller
    {
        private readonly ApplicationDbContext _db = dbContext;
        
        [HttpGet("/")]
        public IActionResult Contacts()
        {
            List<Contact> contacts = [.. _db.Contacts];
            return View(contacts);
        }

        [HttpGet("/GetAll")]
        public IActionResult GetAll() => Json(new { data = _db.Contacts.ToList() });

        [HttpGet("/{id:int}")]
        public IActionResult Display(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
        
        [Authorize(Roles = "Administrator")]
        [HttpDelete("/Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var contactToDelete = _db.Contacts.Find(id);
            if (contactToDelete != null)
            {
                _db.Contacts.Remove(contactToDelete);
                _db.SaveChanges();
                return Json(new { success = true, message="Удаление прошло успешно" });
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
        public IActionResult Edit(Contact contact)
        {
            var q = HttpContext.Request;
            if (ModelState.IsValid)
            {
                _db.Contacts.Add(contact);
                _db.SaveChanges();
                return RedirectToAction("Contacts");
            }
            ViewBag.Editable = true;
            ViewBag.RequestMethod = "put";
            return View(contact);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("/Edit/{id:int}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                return BadRequest();
            }
            var contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound(); 
            }
            ViewBag.RequestMethod = "post";
            return View(contact);
        }
        
        [Authorize(Roles = "Administrator")]
        [HttpPost("/Edit/{id:int}")]
        public IActionResult Edit(int id, Contact contact)
        {
            contact.Id = id;
            if (id == 0)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _db.Contacts.Update(contact);
                _db.SaveChanges();
                return RedirectToAction("Contacts");
            }
            ViewBag.RequestMethod = "post";
            return View(contact);
        }
    }
}
