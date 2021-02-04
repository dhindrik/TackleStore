using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TackleStore.Models;

namespace TackleStore.Clients.Services
{
    public class CategoriesService
    {
        public async static Task<List<Category>> GetCategories()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://tacklestore.azurewebsites.net/api/categories");

            if(!response.IsSuccessStatusCode)
            {
                return new List<Category>();
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<Category>>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }
    }
}
