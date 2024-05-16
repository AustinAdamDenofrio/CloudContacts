using CloudContacts.Data;
using CloudContacts.Models;
using CloudContacts.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudContacts.Services
{
    public class CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : ICategoryRepository
    {
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            context.Categories.Add(category);
            await context.SaveChangesAsync();   

            return category;
        }


        public async Task<IEnumerable<Category>> GetCategoriesAsync(string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            IEnumerable<Category> categories = await context.Categories 
                                                            .Where(c => c.AppUserId == userId)
                                                            .Include(c => c.Contacts)
                                                            .ToListAsync();

            return categories;
        }


        public async Task DeleteCategoryAsync(int categoryId, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Category? category = await context.Categories.FirstOrDefaultAsync(c => c.AppUserId == userId && c.Id == categoryId);

            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }
        public async Task<Category?> GetCategoryByIdAsync(int categoryId, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Category? category = await context.Categories
                                    .Include(c => c.Contacts)
                                    .FirstOrDefaultAsync(c => c.AppUserId == userId && c.Id == categoryId);

            return category;

        }

        public async Task UpdateCategoryAsync(Category category, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            bool shouldUpdate = category.AppUserId == userId && 
                                await context.Categories.AnyAsync(c => c.AppUserId == userId 
                                                                    && c.Id == category.Id);

            if (shouldUpdate)
            {
                context.Categories.Update(category);
                await context.SaveChangesAsync();
            }
        }

    }
}
