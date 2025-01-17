﻿using System.ComponentModel.DataAnnotations;

namespace ContactsNotebook.Models.Identity
{
    public class UserView
    {
        public string? Id { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Register as Admin")]
        public bool IsAdmin { get; set; }
    }
}
