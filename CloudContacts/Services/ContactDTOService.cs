﻿using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;
using CloudContacts.Helper;
using CloudContacts.Models;
using CloudContacts.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CloudContacts.Services
{
    public class ContactDTOService(IContactRepository repository) : IContactDTOService
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

    }
}
