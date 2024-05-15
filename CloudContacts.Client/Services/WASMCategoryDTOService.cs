using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;

namespace CloudContacts.Client.Services
{
    public class WASMCategoryDTOService : ICategoryDTOService
    {
        public Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoriesAsync(int categoryId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailCategoryAsync(int categoryId, EmailData emailData, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDTO>> GetCategoryAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO?> GetCategoryByIdAsyc(int categoryId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(CategoryDTO category, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
