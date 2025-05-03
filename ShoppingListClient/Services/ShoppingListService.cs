using System.Net.Http.Json;
using ShoppingListClient.Models;

namespace ShoppingListClient.Services
{
    public class ShoppingListService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ShoppingListService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://localhost:5254/api/shoppingitems"; // Update this URL based on your server configuration
        }

        public async Task<List<ShoppingItem>> GetShoppingItemsAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ShoppingItem>>() ?? new List<ShoppingItem>();
        }

        public async Task<ShoppingItem> GetShoppingItemAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ShoppingItem>() ?? new ShoppingItem();
        }

        public async Task<ShoppingItem> CreateShoppingItemAsync(ShoppingItem item)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, item);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ShoppingItem>() ?? item;
        }

        public async Task UpdateShoppingItemAsync(ShoppingItem item)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{item.Id}", item);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteShoppingItemAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
