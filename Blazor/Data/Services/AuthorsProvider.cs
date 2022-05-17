using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Data.Services;

namespace Blazor.Data.Services
{
    public class AuthorsProvider : IAuthorProvider
    {
        private HttpClient _client;
        public AuthorsProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Author>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Author>>("/api/author");
        }
        public async Task<Author> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Author>($"/api/author/{id}");
        }
        public async Task<bool> Add(Author item)
        {
            string data = JsonConvert.SerializeObject(item);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/api/author", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }
        public async Task<Author> Edit(Author item)
        {
            string data = JsonConvert.SerializeObject(item);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/api/author", httpContent);
            Author author =
            JsonConvert.DeserializeObject<Author>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(author);
        }
        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/api/author/${id}");
            return await Task.FromResult(delete.IsSuccessStatusCode);
        }
    }
}
