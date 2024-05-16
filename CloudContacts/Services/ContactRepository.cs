using CloudContacts.Data;
using CloudContacts.Models;
using CloudContacts.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudContacts.Services
{
    public class ContactRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IContactRepository
    {
        public async Task AddCategoriesToContactAsync(int contactId, string userId, IEnumerable<int> categoryIds)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Contact? contact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId
                                                                            && c.AppUserId == userId);

            if (contact is not null)
            {
                foreach (int categoryId in categoryIds)
                {
                    Category? category = await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId 
                                                                                        && c.AppUserId == userId);

                    if (category is not null)
                    {
                        contact.Categories.Add(category);
                    }
                }

                await context.SaveChangesAsync();

            }
        }

        public async Task RemoveCategoriesFromContactAsync(int contactId, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Contact? contact = await context.Contacts
                                            .Include(c => c.Categories)
                                            .FirstOrDefaultAsync(c => c.AppUserId == userId && c.Id == contactId);
            if (contact is not null)
            {
                contact.Categories.Clear();
                await context.SaveChangesAsync();
            }
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            context.Contacts.Add(contact);
            await context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact?> GetContactByIdAsync(int contactId, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Contact? contact = await context.Contacts
                                            .Include(c => c.Categories)
                                            .FirstOrDefaultAsync(c => c.AppUserId == userId && c.Id == contactId);

            return contact;
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();
            IEnumerable<Contact> contacts = await context.Contacts
                                                         .Where(c => c.AppUserId == userId)
                                                         .Include(c => c.Categories)
                                                         .ToListAsync();


            return contacts;
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            bool shouldEdit = await context.Contacts.AnyAsync(c => c.AppUserId == contact.AppUserId && c.Id == contact.Id);

            if (shouldEdit)
            {
                //if theres a new image
                // - save the new image
                // - change the contact
                // -delete the old image

                ImageUpload? oldImage = null;

                if (contact.Image is not null)
                {
                    //save the new image
                    context.Images.Add(contact.Image);

                    //check for old image
                    oldImage = await context.Images.FirstOrDefaultAsync(i => i.Id == contact.ImageId);

                    //fix foreign key
                    contact.ImageId = contact.Image.Id;
                }


                // tell the context we want to change this contact
                context.Contacts.Update(contact);
                // save our changes
                await context.SaveChangesAsync();

                if (oldImage is not null)
                {
                    context.Images.Remove(oldImage);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteContactAsync(int contactId, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();
            Contact? contact = context.Contacts.FirstOrDefault(c => c.Id == contactId && c.AppUserId == userId);

            if (contact is not null)
            {
                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Contact>> GetContactsByCategoryIdAsync(int categoryId, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();
            List<Contact> contacts = await context.Contacts
                                                    .Include(c => c.Categories)
                                                    .Where(c => c.AppUserId == userId && c.Categories.Any(cat => cat.Id == categoryId))
                                                    .ToListAsync(); 


            return contacts;
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            string normalizedSearch = searchTerm.Trim().ToLower();

            List<Contact> contacts = await context.Contacts
                                                   .Where(c => c.AppUserId == userId) // only include YOUR contacts
                                                   .Include(c => c.Categories)        // Make sure we brinf the categories with us
                                                   .Where(c => string.IsNullOrEmpty(normalizedSearch)  // Get all contact if they send us a null or empty string
                                                                || c.FirstName!.ToLower().Contains(normalizedSearch)
                                                                || c.LastName!.ToLower().Contains(normalizedSearch)
                                                                || c.Categories.Any(cat => cat.Name!.ToLower().Contains(normalizedSearch))
                                                                )
                                                   .ToListAsync();
            return contacts;
        }
    }
}
