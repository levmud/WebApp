using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Data.Services;
using Blazor.Data.DTOs;

namespace Blazor.Data.Services
{
    public class PublishersProvider : IPublisherProvider
    {
        private HttpClient _client;
        public PublishersProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Publisher>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Publisher>>("/api/publisher");
        }
        public async Task<Publisher> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Publisher>($"/api/publisher/{id}");
        }
        public async Task<bool> Add(PublisherDTO item)
        {
            string data = JsonConvert.SerializeObject(item);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/api/publisher", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }
        public async Task<Publisher> Edit(int id, PublisherDTO item)
        {
            string data = JsonConvert.SerializeObject(item);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/api/publisher/{id}", httpContent);
            Publisher publisher =
            JsonConvert.DeserializeObject<Publisher>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(publisher);
        }
        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/api/publisher/{id}");
            return await Task.FromResult(delete.IsSuccessStatusCode);
        }
    }
}
