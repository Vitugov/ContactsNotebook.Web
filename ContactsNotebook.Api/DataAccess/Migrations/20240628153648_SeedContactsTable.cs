#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsNotebook.Api.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedContactsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "Description", "FirstName", "LastName", "Patronymic", "TelephoneNumber" },
                values: new object[,]
                {
                    { 1, "ул. Ленина, д. 1, Москва", "Описание 1", "Иван", "Иванов", "Иванович", "+7 (913) 111-11-11" },
                    { 2, "ул. Пушкина, д. 2, Санкт-Петербург", "Описание 2", "Петр", "Петров", "Петрович", "+7 (913) 222-22-22" },
                    { 3, "ул. Маяковского, д. 3, Екатеринбург", "Описание 3", "Сидор", "Сидоров", "Сидорович", "+7 (913) 333-33-33" },
                    { 4, "ул. Чехова, д. 4, Новосибирск", "Описание 4", "Кузьма", "Кузнецов", "Кузьмич", "+7 (913) 444-44-44" },
                    { 5, "ул. Толстого, д. 5, Казань", "Описание 5", "Алексей", "Попов", "Алексеевич", "+7 (913) 555-55-55" },
                    { 6, "ул. Достоевского, д. 6, Нижний Новгород", "Описание 6", "Сергей", "Смирнов", "Сергеевич", "+7 (913) 666-66-66" },
                    { 7, "ул. Гоголя, д. 7, Самара", "Описание 7", "Дмитрий", "Козлов", "Дмитриевич", "+7 (913) 777-77-77" },
                    { 8, "ул. Бунина, д. 8, Омск", "Описание 8", "Максим", "Лебедев", "Максимович", "+7 (913) 888-88-88" },
                    { 9, "ул. Тургенева, д. 9, Челябинск", "Описание 9", "Андрей", "Морозов", "Андреевич", "+7 (913) 999-99-99" },
                    { 10, "ул. Лермонтова, д. 10, Ростов-на-Дону", "Описание 10", "Юрий", "Новиков", "Юрьевич", "+7 (913) 101-10-10" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
