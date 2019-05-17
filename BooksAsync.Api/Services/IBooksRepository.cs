using BooksAsync.Api.ExternalModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksAsync.Api.Services
{
    public interface IBooksRepository
    {
        //IEnumerable<Entities.Book> GetBooks();

        //Entities.Author GetBook(Guid id);

        Task<IEnumerable<Entities.Book>> GetBooksAsync();

        Task<BookCover> GetBookCoverAsync(string coverId);

        Task<Entities.Book> GetbookAsync(Guid id);

        void AddBook(Entities.Book bookToAdd);

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<Entities.Book>> GetbooksAsync(IEnumerable<Guid> bookIds);
    }
}
