﻿using ContactsNotebook.DataAccess;
using ContactsNotebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsNotebook.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class ContactsController(ApplicationDbContext dbContext) : Controller
    {
        private readonly ApplicationDbContext _db = dbContext;

        [HttpGet]
        public async Task<IActionResult> Get() => Json(new { data = await _db.Contacts.ToListAsync() });


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var contact = await _db.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Json(contact);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var contactToDelete = await _db.Contacts.FindAsync(id);
            if (contactToDelete != null)
            {
                _db.Contacts.Remove(contactToDelete);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _db.Contacts.Add(contact);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return Json(contact);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int? id, [FromBody] Contact contact)
        {
            if (id == null && id == 0)
            {
                return BadRequest();
            }
            contact.Id = (int)id!;
            if (ModelState.IsValid)
            {
                _db.Contacts.Update(contact);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return Json(contact);
        }
    }
}
