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

        public Task<ContactDTO> CreateContactAsync(ContactDTO contactDTO, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int contactId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailContactAsync(int contactId, EmailData emailData, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ContactDTO?> GetContactByIdAsync(int contactId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsAsync(string userId)
        {
            IEnumerable<ContactDTO> contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDTO>>("api/contacts");
            return contacts;
        }

        public Task<IEnumerable<ContactDTO>> GetContactsByCategoryIdAsync(int categoryId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContactDTO>> SearchContactsAsync(string searchTerm, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContactAsync(ContactDTO contactDTO, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
