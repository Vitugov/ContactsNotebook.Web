using ContactsNotebook.Lib.Models;

namespace ContactsNotebook.Lib.Services.ApiClients.Contacts
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
