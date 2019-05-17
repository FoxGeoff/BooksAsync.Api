using AutoMapper;
using BooksAsync.Api.Filters;
using BooksAsync.Api.Models;
using BooksAsync.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAsync.Api.Controllers
{
    [Route("api/bookscollection")]
    [ApiController]
    public class BookCollectionsController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BookCollectionsController(IBooksRepository booksRepository, IMapper mapper)
        {
            _booksRepository = booksRepository ??
                throw new ArgumentNullException(nameof(booksRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [BooksResultFilter]
        public async Task<IActionResult> CreateBookCollection(
            [FromBody] IEnumerable<BookForCreation> bookCollection)
        {
            var bookEntities = _mapper.Map<IEnumerable<Entities.Book>>(bookCollection);

            foreach (var bookEntity in bookEntities)
            {
                _booksRepository.AddBook(bookEntity);
            }

            await _booksRepository.SaveChangesAsync();

            var booksToReturn = await _booksRepository.GetbooksAsync(
                bookEntities.Select(b => b.Id).ToList());

            return Ok();
        }

        //api/bookcollection/(id1,id2)
        [HttpGet("({bookIds})")]
        [BooksResultFilter]
        public async Task<IActionResult> GetBookCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> bookIds)
        {
            var bookEntities = await _booksRepository.GetbooksAsync(bookIds);

            if (bookIds.Count() != bookEntities.Count())
            {
                return NotFound();
            }

            return Ok(bookEntities);
        }
    }
}
