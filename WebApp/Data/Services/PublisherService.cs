using Microsoft.EntityFrameworkCore;
using WebApp.Data.DTOs;
using WebApp.Data.Models;

namespace WebApp.Data.Services
{
    public class PublisherService
    {
        private MoodContext _context;
        public PublisherService(MoodContext context)
        {
            _context = context;
        }
        public async Task<Publisher?> AddPublisher(PublisherDTO publisher)
        {
            Publisher npublisher = new Publisher
            {
                Name = publisher.Name,
                Phone = publisher.Phone,
                Mail = publisher.Mail,
            };
            //if (publisher.Books.Any())
            //{
            //    npublisher.Books = _context.Books.ToList().IntersectBy(publisher.Books, book =>
            //   book.Id).ToList();
            //}
            var result = _context.Publishers.Add(npublisher);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }


        public async Task<Publisher?> GetPublisher(int id)
        {
            var result = _context.Publishers.Include(a => a.Books).ThenInclude(b => b.Author).FirstOrDefault(author =>
                author.Id == id);

            return await Task.FromResult(result);
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            var result = await _context.Publishers.Include(a => a.Books).ThenInclude(b => b.Author).ToListAsync();
            return await Task.FromResult(result);
        }

        public async Task<List<IncompletePublisherDTO>> GetPublishersIncomplete()
        {
            var authors = await _context.Publishers.ToListAsync();
            List<IncompletePublisherDTO> result = new List<IncompletePublisherDTO>();
            authors.ForEach(p => result.Add(new IncompletePublisherDTO
            {
                Id = p.Id,
                Name= p.Name,
            }));
            return await Task.FromResult(result);
        }

        public async Task<Publisher?> UpdatePublisher(int id, Publisher updatedPublisher)
        {
            var publisher = await
           _context.Publishers.Include(publisher => publisher.Books).FirstOrDefaultAsync(p => p.Id == id);
            if (publisher != null)
            {
                publisher.Name = updatedPublisher.Name;
                publisher.Phone = updatedPublisher.Phone;
                publisher.Mail = updatedPublisher.Mail;
                if (updatedPublisher.Books.Any())
                {
                    publisher.Books = _context.Books.ToList().IntersectBy(updatedPublisher.Books, book => book).ToList();
                }

                _context.Publishers.Update(publisher);
                _context.Entry(publisher).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return publisher;
            }
            return null;
        }
        public async Task<bool> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(au => au.Id == id);
            if (publisher!= null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
