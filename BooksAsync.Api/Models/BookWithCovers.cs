using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAsync.Api.Models
{
    public class BookWithCovers : BookCover
    {
        public IEnumerable<BookCover> bookCovers { get; set; } = new List<BookCover>();
    }
}
