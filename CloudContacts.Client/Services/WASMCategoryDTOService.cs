using CloudContacts.Client.Models;
using CloudContacts.Client.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace CloudContacts.Client.Services
{
    public class WASMCategoryDTOService(HttpClient _httpClient) : ICategoryDTOService
    {
        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO formCategory, string userId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/categories", formCategory);
            response.EnsureSuccessStatusCode();

            CategoryDTO? categoryDTO = await response.Content.ReadFromJsonAsync<CategoryDTO>();
            return categoryDTO!;
        }

        public async Task DeleteCategoryAsync(int categoryId, string userId)
        {
            HttpResponseMessage? response = await _httpClient.DeleteAsync($"api/categories/{categoryId}");
        }

        public async Task<bool> EmailCategoryAsync(int categoryId, EmailData emailData, string userId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/categories/email/{categoryId}", emailData);
            if (response.IsSuccessStatusCode == true)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(string userId)
        {
            IEnumerable<CategoryDTO> categoriesDTO = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDTO>>("api/categories") ?? [];
            return categoriesDTO;
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsyc(int categoryId, string userId)
        {
            CategoryDTO? category = await _httpClient.GetFromJsonAsync<CategoryDTO>($"api/categories/{categoryId}");
            return category;
        }

        public async Task UpdateCategoryAsync(CategoryDTO categoryDTO, string userId)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/categories/{categoryDTO.Id}", categoryDTO);
            response.EnsureSuccessStatusCode();
        }
    }
}
