using ContactsNotebook.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ContactsNotebook.ApiClient
{
    public class ContactsApiClient : IContactsApiClient
    {
        private readonly HttpClient _httpClient;

        public ContactsApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ContactsApiClient");
        }

        public async Task<string> GetContactsAsync()
        {
            var response = await _httpClient.GetAsync($"contacts/get");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"contacts/get/{id}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Contact>(jsonString);
        }

        public async Task<bool> CreateContactAsync(Contact contact)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
            //var contentString = await jsonContent.ReadAsStringAsync();
            var response = await _httpClient.PostAsync($"contacts/add", jsonContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"contacts/update/{contact.Id}", jsonContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"contacts/delete/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
