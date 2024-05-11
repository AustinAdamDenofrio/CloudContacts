using CloudContacts.Client.Models;

namespace CloudContacts.Client.Services.Interfaces
{
    public interface IContactDTOService
    {
        Task<ContactDTO> CreateContactAsync(ContactDTO contactDTO, string userId);
        Task<IEnumerable<ContactDTO>> GetContactsAsync(string userId);
        Task<ContactDTO?> GetContactByIdAsync(int contactId, string userId);
        Task UpdateContactAsync(ContactDTO contactDTO, string userId);

        Task DeleteContactAsync(int contactId, string userId);
    }
}
