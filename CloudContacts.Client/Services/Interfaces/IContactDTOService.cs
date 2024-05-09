using CloudContacts.Client.Models;

namespace CloudContacts.Client.Services.Interfaces
{
    public interface IContactDTOService
    {
        Task<ContactDTO> CreateContactAsync(ContactDTO contactDTO, string userId);
    }
}
