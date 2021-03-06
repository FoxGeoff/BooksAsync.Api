﻿using AutoMapper;
using BooksAsync.Api.ExternalModels;
using BooksAsync.Api.Filters;
using BooksAsync.Api.Models;
using BooksAsync.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [Route("{id}", Name = "GetBook")]
        [BookWithCoversResultFilter]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var bookEntity = await _booksRepository.GetbookAsync(id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            // in production get "dummycover" is from a data store (database)

            // first version just one cover returned
            /* var bookCover = await _booksRepository.GetBookCoverAsync("DummyCover"); */

            // update to get multipal covers one by one
            // dummycover1, dummycover2, dummycover3, dummycover4, dummycover5
            var bookCovers = await _booksRepository.GetBookCoversAsync(id);

            // pass two items into one:

            //#1 method
            /*
            var propertyBag = new Tuple<Entities.Book, IEnumerable<BookCover>>(
                bookEntity, bookCovers);  */
            //#2 method
            /*
            (Entities.Book book, IEnumerable<BookCover> bookCovers) propertyBag = (bookEntity, bookCovers); */
            //#3 method
            return Ok((bookEntity, bookCovers));
        }

        [HttpPost]
        [BookResultFilter]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreation book)
        {
            var bookEntity = _mapper.Map<Entities.Book>(book);
            _booksRepository.AddBook(bookEntity);

            await _booksRepository.SaveChangesAsync();

            //Fetch (refetch) the book from the data store to include author
            await _booksRepository.GetbookAsync(bookEntity.Id);

            return CreatedAtRoute("GetBook",
                new { id = bookEntity.Id },
                bookEntity);
        }
    }
}
