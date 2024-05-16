using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace CloudContacts.Client.Services
{
    public class WASMContactDTOService : IContactDTOService
    {

        private readonly HttpClient _httpClient;

        public WASMContactDTOService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ContactDTO> CreateContactAsync(ContactDTO contactDTO, string userId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/contacts", contactDTO);
            response.EnsureSuccessStatusCode();

            ContactDTO? contact = await response.Content.ReadFromJsonAsync<ContactDTO>();
            return contact!;
        }

        public async Task DeleteContactAsync(int contactId, string userId)
        {
            HttpResponseMessage? response = await _httpClient.DeleteAsync($"api/contacts/{contactId}");
        }

        public async Task<bool> EmailContactAsync(int contactId, EmailData emailData, string userId)
        {

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/contacts/email/{contactId}", emailData);
            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }

            return false;
        }

        public async Task<ContactDTO?> GetContactByIdAsync(int contactId, string userId)
        {
            ContactDTO? contact = await _httpClient.GetFromJsonAsync<ContactDTO>($"api/Contacts/{contactId}");
            return contact;
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsAsync(string userId)
        {
            IEnumerable<ContactDTO> contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDTO>>("api/contacts") ?? [];
            return contacts;
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsByCategoryIdAsync(int categoryId, string userId)
        {
            IEnumerable<ContactDTO> contactsDTO = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDTO>>($"api/contacts?categoryId={categoryId}") ?? [];
            return contactsDTO;
        }

        public async Task<IEnumerable<ContactDTO>> SearchContactsAsync(string searchTerm, string userId)
        {
            IEnumerable<ContactDTO> contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDTO>>($"api/contacts/Search?SearchTerm={searchTerm}") ?? [];
            return contacts;
        }

        public async Task UpdateContactAsync(ContactDTO contactDTO, string userId)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<ContactDTO>($"api/contacts/{contactDTO.Id}", contactDTO);
            response.EnsureSuccessStatusCode();
        }
    }
}
