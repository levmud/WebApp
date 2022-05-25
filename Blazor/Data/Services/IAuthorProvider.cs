using Blazor.Data.Models;
using Blazor.Data.DTOs;



namespace Blazor.Data.Services
{
    public interface IAuthorProvider
    {
        Task<List<Author>> GetAll();
        Task<Author> GetOne(int id);
        Task<bool> Add(AuthorDTO item);
        Task<Author> Edit(int id, AuthorDTO item);
        Task<bool> Remove(int id);
    }
}
