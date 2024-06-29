﻿// <auto-generated />
using ContactsNotebook.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactsNotebook.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContactsNotebook.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "ул. Ленина, д. 1, Москва",
                            Description = "Описание 1",
                            FirstName = "Иван",
                            LastName = "Иванов",
                            Patronymic = "Иванович",
                            TelephoneNumber = "+7 (913) 111-11-11"
                        },
                        new
                        {
                            Id = 2,
                            Address = "ул. Пушкина, д. 2, Санкт-Петербург",
                            Description = "Описание 2",
                            FirstName = "Петр",
                            LastName = "Петров",
                            Patronymic = "Петрович",
                            TelephoneNumber = "+7 (913) 222-22-22"
                        },
                        new
                        {
                            Id = 3,
                            Address = "ул. Маяковского, д. 3, Екатеринбург",
                            Description = "Описание 3",
                            FirstName = "Сидор",
                            LastName = "Сидоров",
                            Patronymic = "Сидорович",
                            TelephoneNumber = "+7 (913) 333-33-33"
                        },
                        new
                        {
                            Id = 4,
                            Address = "ул. Чехова, д. 4, Новосибирск",
                            Description = "Описание 4",
                            FirstName = "Кузьма",
                            LastName = "Кузнецов",
                            Patronymic = "Кузьмич",
                            TelephoneNumber = "+7 (913) 444-44-44"
                        },
                        new
                        {
                            Id = 5,
                            Address = "ул. Толстого, д. 5, Казань",
                            Description = "Описание 5",
                            FirstName = "Алексей",
                            LastName = "Попов",
                            Patronymic = "Алексеевич",
                            TelephoneNumber = "+7 (913) 555-55-55"
                        },
                        new
                        {
                            Id = 6,
                            Address = "ул. Достоевского, д. 6, Нижний Новгород",
                            Description = "Описание 6",
                            FirstName = "Сергей",
                            LastName = "Смирнов",
                            Patronymic = "Сергеевич",
                            TelephoneNumber = "+7 (913) 666-66-66"
                        },
                        new
                        {
                            Id = 7,
                            Address = "ул. Гоголя, д. 7, Самара",
                            Description = "Описание 7",
                            FirstName = "Дмитрий",
                            LastName = "Козлов",
                            Patronymic = "Дмитриевич",
                            TelephoneNumber = "+7 (913) 777-77-77"
                        },
                        new
                        {
                            Id = 8,
                            Address = "ул. Бунина, д. 8, Омск",
                            Description = "Описание 8",
                            FirstName = "Максим",
                            LastName = "Лебедев",
                            Patronymic = "Максимович",
                            TelephoneNumber = "+7 (913) 888-88-88"
                        },
                        new
                        {
                            Id = 9,
                            Address = "ул. Тургенева, д. 9, Челябинск",
                            Description = "Описание 9",
                            FirstName = "Андрей",
                            LastName = "Морозов",
                            Patronymic = "Андреевич",
                            TelephoneNumber = "+7 (913) 999-99-99"
                        },
                        new
                        {
                            Id = 10,
                            Address = "ул. Лермонтова, д. 10, Ростов-на-Дону",
                            Description = "Описание 10",
                            FirstName = "Юрий",
                            LastName = "Новиков",
                            Patronymic = "Юрьевич",
                            TelephoneNumber = "+7 (913) 101-10-10"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
