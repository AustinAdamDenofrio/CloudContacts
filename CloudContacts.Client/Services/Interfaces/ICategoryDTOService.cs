using CloudContacts.Client.Models;

namespace CloudContacts.Client.Services.Interfaces
{
    public interface ICategoryDTOService
    {
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId);
        Task<IEnumerable<CategoryDTO>> GetCategoryAsync(string userId);

        Task<CategoryDTO?> GetCategoryByIdAsyc(int categoryId, string userId);
        Task DeleteCategoriesAsync(int categoryId, string userId);
        Task UpdateCategoryAsync(CategoryDTO category, string userId);
    }
}
