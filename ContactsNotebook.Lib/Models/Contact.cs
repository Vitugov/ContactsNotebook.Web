using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactsNotebook.Lib.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "{0} является обязательной")]
        [MaxLength(25)]
        public string LastName { get; set; } = "";

        [DisplayName("Имя")]
        [Required(ErrorMessage = "{0} является обязательным")]
        [MaxLength(25)]
        public string FirstName { get; set; } = "";

        [MaxLength(25)]
        [DisplayName("Отчество")]
        public string? Patronymic { get; set; } = "";

        [DisplayName("Номер телефона")]
        [Required(ErrorMessage = "{0} является обязательным")]
        [MaxLength(25)]
        public string TelephoneNumber { get; set; } = "";

        [DisplayName("Адрес")]
        [MaxLength(100)]
        public string? Address { get; set; } = "";
        [DisplayName("Описание")]
        [MaxLength(200)]
        public string? Description { get; set; } = "";

        public Contact() { }
        public Contact(int id, string lastName, string firstName, string patronymic, string telephoneNumber, string address, string description)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            TelephoneNumber = telephoneNumber;
            Address = address;
            Description = description;
        }
    }
}
