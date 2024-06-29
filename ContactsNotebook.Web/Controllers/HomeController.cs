using ContactsNotebook.DataAccess;
using ContactsNotebook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ContactsNotebook.Web.Controllers
{
    public class HomeController(ApplicationDbContext dbContext) : Controller
    {
        private readonly ApplicationDbContext _db = dbContext;
        
        [HttpGet]
        [Route("/")]
        public IActionResult Contacts()
        {
            List<Contact> contacts = [.. _db.Contacts];
            return View(contacts);
        }

        [HttpGet]
        [Route("/GetAll")]
        public IActionResult GetAll() => Json(new { data = _db.Contacts.ToList() });

        [HttpDelete]
        [Route("/Delete/{id:int}")]
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

        [HttpGet]
        [Route("/create")]
        public IActionResult Create()
        {
            ViewBag.Editable = true;
            ViewBag.Route = "/create";
            ViewBag.RequestMethod = "put";
            return View("Edit");
        }

        [HttpPut]
        [Route("/create")]
        public IActionResult Create([FromBody] Contact contact)
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
            ViewBag.Route = "/create";
            return View("Edit", contact);
        }

        [HttpGet]
        [Route("/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            var contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound(); 
            }
            ViewBag.Editable = false;
            ViewBag.Route = $"/{id}";
            ViewBag.RequestMethod = "post";
            return View(contact);
        }

        [HttpPost]
        [Route("/{id}")]
        public IActionResult Edit(int id, Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Id = id;
                _db.Contacts.Update(contact);
                _db.SaveChanges();
                return RedirectToAction("Contacts");
            }
            ViewBag.Editable = true;
            ViewBag.Route = $"/{contact.Id}";
            ViewBag.RequestMethod = "post";
            return View(contact);
        }
    }
}
