using ContactsNotebook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;

namespace ContactsNotebook.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {  
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(new List<Contact>
        {
            new(1, "Иванов", "Иван", "Иванович", "+7 (913) 111-11-11", "ул. Ленина, д. 1, Москва", "Описание 1"),
            new(2, "Петров", "Петр", "Петрович", "+7 (913) 222-22-22", "ул. Пушкина, д. 2, Санкт-Петербург", "Описание 2"),
            new(3, "Сидоров", "Сидор", "Сидорович", "+7 (913) 333-33-33", "ул. Маяковского, д. 3, Екатеринбург", "Описание 3"),
            new(4, "Кузнецов", "Кузьма", "Кузьмич", "+7 (913) 444-44-44", "ул. Чехова, д. 4, Новосибирск", "Описание 4"),
            new(5, "Попов", "Алексей", "Алексеевич", "+7 (913) 555-55-55", "ул. Толстого, д. 5, Казань", "Описание 5"),
            new(6, "Смирнов", "Сергей", "Сергеевич", "+7 (913) 666-66-66", "ул. Достоевского, д. 6, Нижний Новгород", "Описание 6"),
            new(7, "Козлов", "Дмитрий", "Дмитриевич", "+7 (913) 777-77-77", "ул. Гоголя, д. 7, Самара", "Описание 7"),
            new(8, "Лебедев", "Максим", "Максимович", "+7 (913) 888-88-88", "ул. Бунина, д. 8, Омск", "Описание 8"),
            new(9, "Морозов", "Андрей", "Андреевич", "+7 (913) 999-99-99", "ул. Тургенева, д. 9, Челябинск", "Описание 9"),
            new(10, "Новиков", "Юрий", "Юрьевич", "+7 (913) 101-10-10", "ул. Лермонтова, д. 10, Ростов-на-Дону", "Описание 10")
        });
        }
    }
}
