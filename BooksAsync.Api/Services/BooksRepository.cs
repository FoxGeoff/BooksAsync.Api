using BooksAsync.Api.Contexts;
using BooksAsync.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAsync.Api.Services
{
    public class BooksRepository : IBooksRepository, IDisposable
    {
        private BooksContext _context;

        public BooksRepository(BooksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
    }
}
