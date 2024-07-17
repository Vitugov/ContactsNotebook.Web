using ContactsNotebook.Models;

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
