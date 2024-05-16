using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;
using CloudContacts.Models;
using CloudContacts.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CloudContacts.Services
{
    public class CategoryDTOService(ICategoryRepository repository, IEmailSender emailSender) : ICategoryDTOService
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


        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(string userId)
        {
            // get from the database
            IEnumerable<Category> createCategories = await repository.GetCategoriesAsync(userId);

           IEnumerable<CategoryDTO> categoryDTO = createCategories.Select(c=>c.ToDTO());


            return categoryDTO;
        }


        public async Task DeleteCategoryAsync(int categoryId, string userId)
        {
            await repository.DeleteCategoryAsync(categoryId, userId);
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsyc(int categoryId, string userId)
        {
            Category? category = await repository.GetCategoryByIdAsync(categoryId, userId);
            return category?.ToDTO();
        }

        public async Task UpdateCategoryAsync(CategoryDTO category, string userId)
        {
            Category? categoryToUpdate = await repository.GetCategoryByIdAsync(category.Id, userId);

            if (categoryToUpdate is not null)
            {
                categoryToUpdate.Name = category.Name;

                categoryToUpdate.Contacts.Clear();

                await repository.UpdateCategoryAsync(categoryToUpdate, userId);
            }
        }

        public async Task<bool> EmailCategoryAsync(int categoryId, EmailData emailData, string userId)
        {
            try
            {
                Category? category = await repository.GetCategoryByIdAsync(categoryId, userId);
                if (category is null || category.Contacts.Count < 1) return false;

                string recipients = string.Join(";", category.Contacts.Select(c => c.Email!));
                await emailSender.SendEmailAsync(recipients, emailData.Subject!, emailData.Message!);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            };
        }


    }
}
