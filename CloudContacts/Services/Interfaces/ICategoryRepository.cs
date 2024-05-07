using CloudContacts.Models;

namespace CloudContacts.Services.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<IEnumerable<Category>> GetCategoryAsync(string userId);
    }
}
