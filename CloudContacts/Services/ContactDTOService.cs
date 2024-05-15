using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;
using CloudContacts.Helper;
using CloudContacts.Models;
using CloudContacts.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace CloudContacts.Services
{
    public class ContactDTOService(IContactRepository repository, IEmailSender emailSender) : IContactDTOService
    {
        public async Task<ContactDTO> CreateContactAsync(ContactDTO contactDTO, string userId)
        {
            
            Contact newContact = new Contact()
            {
                FirstName = contactDTO.FirstName,
                LastName = contactDTO.LastName,
                Email = contactDTO.Email,
                BirthDate = contactDTO.BirthDate,
                Address = contactDTO.Address,
                Address2 = contactDTO.Address2,
                City = contactDTO.City,
                State = contactDTO.State,
                ZipCode = contactDTO.ZipCode,
                PhoneNumber = contactDTO.PhoneNumber,
                Created = DateTimeOffset.Now,
                AppUserId = userId,

            };

            //TODO: Images
            if (contactDTO.ImageUrl?.StartsWith("data:") == true)
            {
                newContact.Image = UploadHelper.GetImageUpload(contactDTO.ImageUrl);
            }



            //Send it to Repository
            Contact createContact = await repository.CreateContactAsync(newContact);
            //ToDo: DTO categories
            IEnumerable<int> categoryIds = contactDTO.Categories.Select(c => c.Id);
            await repository.AddCategoriesToContactAsync(createContact.Id, userId, categoryIds);


            return createContact.ToDTO();

        }

        public async Task<ContactDTO?> GetContactByIdAsync(int contactId, string userId)
        {
            Contact? contact = await repository.GetContactByIdAsync(contactId, userId);
            // turnary in place of if statement
            return contact?.ToDTO(); 
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsAsync(string userId)
        {
            IEnumerable<Contact> contacts = await repository.GetContactsAsync(userId);
            IEnumerable<ContactDTO> contactsDTO = contacts.Select(c => c.ToDTO());

            return contactsDTO;
        }

        public async Task UpdateContactAsync(ContactDTO contactDTO, string userId)
        {
            Contact? contact = await repository.GetContactByIdAsync(contactDTO.Id, userId);

            if (contact is not null) 
            {
                contact.FirstName = contactDTO.FirstName;
                contact.LastName = contactDTO.LastName;
                contact.BirthDate = contactDTO.BirthDate;
                contact.Address = contactDTO.Address;
                contact.Address2 = contactDTO.Address2;
                contact.City = contactDTO.City;
                contact.State = contactDTO.State;
                contact.ZipCode = contactDTO.ZipCode;
                contact.Email = contactDTO.Email;
                contact.PhoneNumber = contactDTO.PhoneNumber;

                if (contactDTO.ImageUrl?.StartsWith("data:") == true)
                {
                    contact.Image = UploadHelper.GetImageUpload(contactDTO.ImageUrl);
                }
                else 
                { 
                    contact.Image = null;
                }

                // dont let db update cats yet
                contact.Categories.Clear();
                await repository.UpdateContactAsync(contact);

                //remove all the old cats
                await repository.RemoveCategoriesFromContactAsync(contact.Id, userId);

                //add back the cats based on the users selected 
                IEnumerable<int> selectedCategoryIds = contactDTO.Categories.Select(c => c.Id);
                await repository.AddCategoriesToContactAsync(contact.Id, userId, selectedCategoryIds);
            }
        }

        public async Task DeleteContactAsync(int contactId, string userId)
        {
            await repository.DeleteContactAsync(contactId, userId);
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsByCategoryIdAsync(int categoryId, string userId)
        {
            IEnumerable<Contact> contacts = await repository.GetContactsByCategoryIdAsync(categoryId, userId);

            return contacts.Select(c => c.ToDTO());
        }

        public async Task<IEnumerable<ContactDTO>> SearchContactsAsync(string searchTerm, string userId)
        {
            IEnumerable<Contact> contacts = await repository.SearchContactsAsync(searchTerm, userId);
            return contacts.Select(c => c.ToDTO());
        }

        public async Task<bool> EmailContactAsync(int contactId, EmailData emailData, string userId)
        {
            Contact? contact = await repository.GetContactByIdAsync(contactId, userId);
            //if there was no email, or cannot find a contact with that id in DB for the logged in user.
            if (contact is null || contact.Email.IsNullOrEmpty()) return false;
            //If contact has email
            await emailSender.SendEmailAsync(contact.Email!, emailData.Subject!, emailData.Message!);
            return true;
        }
    }
}
