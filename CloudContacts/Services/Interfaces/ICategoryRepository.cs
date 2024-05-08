using CloudContacts.Models;

namespace CloudContacts.Services.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<IEnumerable<Category>> GetCategoryAsync(string userId);

        Task<Category?> GetCategoryByIdAsync(int categoryId, string userId);

        Task UpdateCategoryAsync(Category category, string userId);

        Task DeleteCategoriesAsync(int categoryId, string userId);
    }
}
