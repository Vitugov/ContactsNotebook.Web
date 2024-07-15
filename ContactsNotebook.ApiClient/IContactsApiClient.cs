using ContactsNotebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsNotebook.ApiClient
{
    public interface IContactsApiClient
    {
        Task<string> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task<bool> CreateContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(int id);
    }
}
