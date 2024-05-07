using CloudContacts.Client.Models;

namespace CloudContacts.Client.Services.Interfaces
{
    public interface ICategoryDTOService
    {
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId);
        Task<IEnumerable<CategoryDTO>> GetCategoryAsync(string userId);
    }
}
