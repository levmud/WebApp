using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Data.Services;

namespace Blazor.Data.Services
{
    public class BooksProvider : IBookProvider
    {
        private HttpClient _client;
        public BooksProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Book>>("/api/book");
        }
        public async Task<Book> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Book>($"/api/book/{id}");
        }
        public async Task<bool> Add(Book item)
        {
            string data = JsonConvert.SerializeObject(item);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/api/author", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }
        public async Task<Book> Edit(Book item)
        {
            string data = JsonConvert.SerializeObject(item);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/api/book", httpContent);
            Book book =
            JsonConvert.DeserializeObject<Book>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(book);
        }
        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/api/book/{id}");
            return await Task.FromResult(delete.IsSuccessStatusCode);
        }
    }
}
