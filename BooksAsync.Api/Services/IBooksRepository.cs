using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAsync.Api.Services
{
    public interface IBooksRepository
    {
        //IEnumerable<Entities.Book> GetBooks();

        //Entities.Author GetBook(Guid id);

        Task<IEnumerable<Entities.Book>> GetBooksAsync();

        Task<Entities.Book> GetbookAsync(Guid id);
    }
}
