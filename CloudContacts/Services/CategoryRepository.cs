﻿using CloudContacts.Data;
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

        public async Task<IEnumerable<Category>> GetCategoryAsync(string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            IEnumerable<Category> categories = await context.Categories.Where(c => c.AppUserId == userId).ToListAsync();

            return categories;
        }
    }
}