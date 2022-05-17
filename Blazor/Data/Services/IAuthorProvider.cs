﻿using Blazor.Data.Models;


namespace Blazor.Data.Services
{
    public interface IAuthorProvider
    {
        Task<List<Author>> GetAll();
        Task<Author> GetOne(int id);
        Task<bool> Add(Author item);
        Task<Author> Edit(Author item);
        Task<bool> Remove(int id);
    }
}
