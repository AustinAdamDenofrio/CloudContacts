using Microsoft.AspNetCore.Authorization;
using CloudContacts.Helper.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CloudContacts.Data;
using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;
using CloudContacts.Models;

namespace CloudContacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController(IContactDTOService _contactService) : ControllerBase
    {
        private string _userId => User.GetUserId()!; // [authorize] means userId cannot be null


        // GET: "api/contacts" OR "api/contacts?categoryId=4" -> list of user contacts, optionally filtered by category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContacts([FromQuery] int? categoryId)
        {
            try
            {
                if (categoryId == null)
                {
                    IEnumerable<ContactDTO> contactsDTO = await _contactService.GetContactsAsync(_userId);
                    return Ok(contactsDTO);
                }
                else
                {
                    IEnumerable<ContactDTO> contactsDTO = await _contactService.GetContactsByCategoryIdAsync(categoryId.Value, _userId);
                    return Ok(contactsDTO);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        //    // GET: "api/contacts/5" -> a contact or 404
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactDTO?>> GetContactById([FromRoute] int id)
        {
            try
            {
                ContactDTO? contact = await _contactService.GetContactByIdAsync(id, _userId);

                if (contact == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(contact);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // GET: "api/contacts/search?query=whatever" -> contacts matching the search query
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> SearchContacts([FromQuery] string searchTerm)
        {
            try
            {
                IEnumerable<ContactDTO> contactsDTO = await _contactService.SearchContactsAsync(searchTerm, _userId);

                return Ok(contactsDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        //    // POST: "api/contacts" -> creates and returns the created contact
        [HttpPost]
        public async Task<ActionResult<ContactDTO>> CreateContact([FromBody] ContactDTO contactDTO)
        {
            try
            {
                ContactDTO contact = await _contactService.CreateContactAsync(contactDTO, _userId);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        //    // PUT: "api/contacts/5" -> updates the selected contact and returns Ok
        [HttpPut("{contactDTO.Id:int}")]
        public async Task<ActionResult> UpdateContact(ContactDTO contactDTO)
        {
            try
            {
                await _contactService.UpdateContactAsync(contactDTO, _userId);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        //    // DELETE: "api/contacts/5" -> deletes the selected contact and returns NoContent
        [HttpDelete("{contactId:int}")]
        public async Task<ActionResult> DeleteContact([FromRoute] int contactId)
        {
            try
            {
                await _contactService.DeleteContactAsync(contactId, _userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        //    // POST: "api/contacts/5/email" -> sends an email to contact and returns Ok or BadRequest to indicate success or failure
        [HttpPost("{Id:int}")]
        public async Task<ActionResult> EmailContact([FromRoute] int Id, EmailData emailData)
        {
            try
            {
                await _contactService.EmailContactAsync(Id, emailData, _userId);
                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }
    }
}
