using AutoMapper;
using BooksAsync.Api.Filters;
using BooksAsync.Api.Models;
using BooksAsync.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BooksAsync.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BooksController(IBooksRepository booksRepository, IMapper mapper)
        {
            _booksRepository = booksRepository
                ?? throw new ArgumentNullException(nameof(booksRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [BooksResultFilter]
        public async Task<IActionResult> GetBooks()
        {
            var bookEntities = await _booksRepository.GetBooksAsync();
            return Ok(bookEntities);
        }

        [HttpGet]
        [BookResultFilter]
        [Route("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var bookEntity = await _booksRepository.GetbookAsync(id);
            if (bookEntity == null)
            {
                return NotFound();
            }
            return Ok(bookEntity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreation book)
        {
            var bookEntity = _mapper.Map<Entities.Book>(book);
            _booksRepository.AddBook(bookEntity);

            await _booksRepository.SaveChangesAsync();

            return CreatedAtRoute("GetBook",
                new { id = bookEntity.Id },
                bookEntity);
        }
    }
}
