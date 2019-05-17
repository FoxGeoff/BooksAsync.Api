using BooksAsync.Api.Contexts;
using BooksAsync.Api.Entities;
using BooksAsync.Api.ExternalModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BooksAsync.Api.Services
{
    public class BooksRepository : IBooksRepository, IDisposable
    {
        private BooksContext _context;
        private IHttpClientFactory _httpClientFactory;

        public BooksRepository(BooksContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<Book> GetbookAsync(Guid id)
        {
            return await _context.Books
                .Where(b => b.Id == id)
                .Include(b => b.Author)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        //Per api instructions don't use .AddAsync()
        public void AddBook(Book bookToAdd)
        {
            if (bookToAdd == null)
                throw new ArgumentNullException(nameof(bookToAdd));

            _context.Add(bookToAdd);
        }

        public async Task<bool> SaveChangesAsync()
        {
            //return true if 1 or more entities are changed
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<IEnumerable<Entities.Book>> GetbooksAsync(IEnumerable<Guid> bookIds)
        {
            return await _context.Books.Where(b => bookIds.Contains(b.Id))
                .Include(b => b.Author).ToListAsync();
        }

        public async Task<BookCover> GetBookCoverAsync(string coverId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            //pass though a dummy name
            var response = await httpClient
                .GetAsync($"http://localhost:52644/api/bookcovers/{coverId}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BookCover>(
                    await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
