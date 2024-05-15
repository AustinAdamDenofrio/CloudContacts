using Microsoft.AspNetCore.Authorization;
using CloudContacts.Helper.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CloudContacts.Data;
using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;

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
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactsAsync([FromRoute] int categoryId, string userId)
        {
            try
            {
                IEnumerable<ContactDTO> contactDTO = await _contactService.GetContactsAsync(userId);

                if (contactDTO is not null) 
                { 
                  return Ok(contactDTO);                
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        //    // GET: "api/contacts/5" -> a contact or 404
        //    public async Task<ActionResult<>> GetContactById()
        //    {
        //        try
        //        {

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return Problem();
        //        }
        //    }

        //    // GET: "api/contacts/search?query=whatever" -> contacts matching the search query
        //    public async Task<ActionResult<>> SearchContacts()
        //    {
        //        try
        //        {

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return Problem();
        //        }
        //    }

        //    // POST: "api/contacts" -> creates and returns the created contact
        //    public async Task<ActionResult<>> CreateContact()
        //    {
        //        try
        //        {

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return Problem();
        //        }
        //    }

        //    // PUT: "api/contacts/5" -> updates the selected contact and returns Ok
        //    public async Task<ActionResult> UpdateContact()
        //    {
        //        try
        //        {

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return Problem();
        //        }
        //    }

        //    // DELETE: "api/contacts/5" -> deletes the selected contact and returns NoContent
        //    public async Task<ActionResult> DeleteContact()
        //    {
        //        try
        //        {

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return Problem();
        //        }
        //    }

        //    // POST: "api/contacts/5/email" -> sends an email to contact and returns Ok or BadRequest to indicate success or failure
        //    public async Task<ActionResult> EmailContact()
        //    {
        //        try
        //        {

        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //            return Problem();
        //        }
        //    }
    }
}
