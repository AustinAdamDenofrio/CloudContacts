using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;
using CloudContacts.Helper.Extensions;
using CloudContacts.Models;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            try
            {
                IEnumerable<CategoryDTO> categoriesDTO = await _categoryService.GetCategoriesAsync(_userId);
                return Ok(categoriesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // GET: "api/categories/5" -> returns a category or 404
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory([FromRoute] int id)
        {
            try
            {
                CategoryDTO? category = await _categoryService.GetCategoryByIdAsyc(id, _userId);

                if (category == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(category);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // POST: "api/categories" -> creates a category and returns the created category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryDTO category)
        {
            try
            {
                CategoryDTO contact = await _categoryService.CreateCategoryAsync(category, _userId);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // PUT: "api/categories/5" -> updates the selected category and returns Ok
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryDTO category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest();
                }
                else
                {
                    await _categoryService.UpdateCategoryAsync(category, _userId);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // DELETE: "api/categories/5" -> deletes the selected category and returns NoContent
        [HttpDelete("{categoryId:int}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(categoryId, _userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Problem();
            }
        }

        // POST: "api/categories/5/email" -> sends an email to category and returns Ok or BadRequest to indicate success or failure
        [HttpPost("email/{categoryId:int}")]
        public async Task<ActionResult> EmailCategory([FromRoute] int categoryId, [FromBody] EmailData emailData)
        {
            try
            {
                await _categoryService.EmailCategoryAsync(categoryId, emailData, _userId);
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
