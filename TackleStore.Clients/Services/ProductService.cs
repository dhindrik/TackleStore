using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TackleStore.Models;

namespace TackleStore.Clients.Services
{
    public class ProductService
    {
        private static List<Product> products;

        public async static Task<List<Product>> Get(int categoryId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://tacklestore.azurewebsites.net/api/ProductsinCategory?category={categoryId}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Product>();
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            products = result;

            return result;
        }

        public static async Task<ProductItem> GetProduct(string id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://tacklestore.azurewebsites.net/api/GetProduct?id={id}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ProductItem>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public static async Task<List<SearchResultItem>> Search(string text)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://tacklestore.azurewebsites.net/api/SearchProducts?text={text}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<SearchResultItem>>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }
    }
}
