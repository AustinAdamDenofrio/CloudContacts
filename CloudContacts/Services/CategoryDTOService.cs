using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;
using CloudContacts.Models;
using CloudContacts.Services.Interfaces;

namespace CloudContacts.Services
{
    public class CategoryDTOService(ICategoryRepository repository) : ICategoryDTOService
    {
        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId)
        {
            Category newCategory = new Category()
            {
                Name = category.Name,
                AppUserId = userId,
            };

            Category createdCategory = await repository.CreateCategoryAsync(newCategory);

            return createdCategory.ToDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoryAsync(string userId)
        {
            // get from the database
            IEnumerable<Category> createCategories = await repository.GetCategoryAsync(userId);
            //create a list to hold DTOS
            List<CategoryDTO> results = new List<CategoryDTO>();

            //convert to DTO
            foreach (Category category in createCategories)
            {
                results.Add(category.ToDTO());
            }

            return results;
        }
    }
}
