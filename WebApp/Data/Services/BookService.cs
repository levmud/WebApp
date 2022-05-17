using Microsoft.EntityFrameworkCore;
using WebApp.Data.DTOs;
using WebApp.Data.Models;

namespace WebApp.Data.Services
{
    public class BookService
    {
        private MoodContext _context;
        public BookService(MoodContext context)
        {
            _context = context;
        }
        public async Task<Book?> AddBook(BookDTO bookDTO)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(au => au.Id == bookDTO.Author);
            if (author == null)
                return null;
            var publisher = await _context.Publishers.FirstOrDefaultAsync(pub => pub.Id == bookDTO.Publisher);
            if (publisher == null)
                return null;

            Book nbook = new Book
            {
                Title = bookDTO.Title,
                Genre = bookDTO.Genre,
                Cost = bookDTO.Cost,
                Description = bookDTO.Description,
                Author = author,
                Publisher = publisher
            };


            var result = _context.Books.Add(nbook);
            await _context.SaveChangesAsync();

            // bookDTO.Id = result.Entity.Id;
            return await Task.FromResult(result.Entity);
        }


        public async Task<Book?> GetBook(int id)
        {
            var result = _context.Books.Include(b => b.Author).Include(b => b.Publisher).FirstOrDefault(book =>
                book.Id == id);

            return await Task.FromResult(result);
        }

        public async Task<List<Book>> GetBooks()
        {
            var result = await _context.Books.Include(b => b.Author).Include(b => b.Publisher).ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<List<IncompleteBookDTO>> GetBooksIncomplete()
        {
            var books = await _context.Books.ToListAsync();
            List<IncompleteBookDTO> result = new List<IncompleteBookDTO>();
            books.ForEach(b => result.Add(new IncompleteBookDTO
            {
                Id = b.Id,
                Title = b.Title,
            }));
            return await Task.FromResult(result);
        }

        public async Task<BookDTO?> UpdateBook(int id, BookDTO updatedBook)
        {
            var book = await _context.Books
                .Include(book => book.Author).Include(b => b.Publisher).FirstOrDefaultAsync(au => au.Id == id);
            if (book == null)
                return null;

            book.Title = updatedBook.Title;
            book.Genre = updatedBook.Genre;
            book.Cost = updatedBook.Cost;
            book.Description = updatedBook.Description;


            _context.Books.Update(book);
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await Task.FromResult(updatedBook);

        }
        public async Task<bool> DeleteBook(int id)
        {
            var book= await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
