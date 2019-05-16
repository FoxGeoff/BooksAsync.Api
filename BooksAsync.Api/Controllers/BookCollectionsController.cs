using BooksAsync.Api.Models;
using BooksAsync.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAsync.Api.Controllers
{
    public class BookCollectionsController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public BookCollectionsController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository ??
                throw new ArgumentNullException(nameof(booksRepository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooksCollection([FromBody] IEnumerable<BookForCreation> books)
        {

            return Ok();
        }
    }
}
