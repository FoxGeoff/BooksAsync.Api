using System.Collections.Generic;

namespace BooksAsync.Api.Models
{
    public class BookWithCovers : Book
    {
        public IEnumerable<BookCover> bookCovers { get; set; } = new List<BookCover>();
    }
}
