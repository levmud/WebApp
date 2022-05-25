using Blazor.Data.Models;
using Blazor.Data.DTOs;

namespace Blazor.Data.Services
{
    public interface IPublisherProvider
    {
        Task<List<Publisher>> GetAll();
        Task<Publisher> GetOne(int id);
        Task<bool> Add(PublisherDTO item);
        Task<Publisher> Edit(int id, PublisherDTO item);
        Task<bool> Remove(int id);
    }
}