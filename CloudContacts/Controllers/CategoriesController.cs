using CloudContacts.Client.Services.Interfaces;
using CloudContacts.Helper.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CloudContacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController(ICategoryDTOService _categoryService) : ControllerBase
    {
        private string _userId => User.GetUserId()!; // [authorize] means userId cannot be null

        // GET: "api/categories" -> returns the users categories
        public async Task<ActionResult<>> GetCategories()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // GET: "api/categories/5" -> returns a category or 404
        public async Task<ActionResult<>> GetCategory()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // POST: "api/categories" -> creates a category and returns the created category
        public async Task<ActionResult<>> CreateCategory()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // PUT: "api/categories/5" -> updates the selected category and returns Ok
        public async Task<ActionResult> UpdateCategory()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // DELETE: "api/categories/5" -> deletes the selected category and returns NoContent
        public async Task<ActionResult> DeleteCategory()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // POST: "api/categories/5/email" -> sends an email to category and returns Ok or BadRequest to indicate success or failure
        public async Task<ActionResult> EmailCategory()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }
    }
}
