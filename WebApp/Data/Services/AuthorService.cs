using Microsoft.EntityFrameworkCore;
using WebApp.Data.DTOs;
using WebApp.Data.Models;

namespace WebApp.Data.Services
{
    public class AuthorService
    {
        private MoodContext _context;
        public AuthorService(MoodContext context)
        {
            _context = context;
        }
        public async Task<Author?> AddAuthor(AuthorDTO author)
        {
            Author nauthor = new Author
            {
                Fullname = author.Fullname,
                Country = author.Country,
                Direction = author.Direction
            };
            //if (author.Books.Any())
            //{
            //    nauthor.Books = _context.Books.ToList().IntersectBy(author.Books, book =>
            //   book.Id).ToList();
            //}
            var result = _context.Authors.Add(nauthor);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }


        public async Task<Author?> GetAuthor(int id)
        {
            var result = _context.Authors.Include(a => a.Books).ThenInclude(b => b.Publisher).FirstOrDefault(author =>
                author.Id == id);

            return await Task.FromResult(result);
        }

        public async Task<List<Author>> GetAuthors()
        {
            var result = await _context.Authors.Include(a => a.Books).ThenInclude(b => b.Publisher).ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<List<IncompleteAuthorDTO>> GetAuthorsIncomplete()
        {
            var authors = await _context.Authors.ToListAsync();
            List<IncompleteAuthorDTO> result = new List<IncompleteAuthorDTO>();
            authors.ForEach(au => result.Add(new IncompleteAuthorDTO
            {
                Id = au.Id,
                Fullname = au.Fullname,
            }));
            return await Task.FromResult(result);
        }

        public async Task<Author?> UpdateAuthor(int id, Author updatedAuthor)
        {
            var author = await
           _context.Authors.Include(author => author.Books).FirstOrDefaultAsync(au => au.Id == id);
            if (author != null)
            {
                author.Fullname = updatedAuthor.Fullname;
                author.Country = updatedAuthor.Country;
                author.Direction = updatedAuthor.Direction;
                if (updatedAuthor.Books.Any())
                {
                    author.Books = _context.Books.ToList().IntersectBy(updatedAuthor.Books, book => book).ToList();
                }

                _context.Authors.Update(author);
                _context.Entry(author).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return author;
            }
            return null;
        }
        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(au => au.Id == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
