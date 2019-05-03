using BooksAsync.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAsync.Api.Contexts
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seed Database
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = Guid.Parse("d2888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "George",
                    LastName = "RR Martin"
                },
                new Author()
                {
                    Id = Guid.Parse("fbb5224a-518c-4f09-89d9-a805af0f28f0"),
                    FirstName = "Stephen",
                    LastName = "Fry"
                },
                new Author()
                {
                    Id = Guid.Parse("895a362c-1f4a-4ba7-8f5f-24f810600dfa"),
                    FirstName = "Doglas",
                    LastName = "Adams"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    Id = Guid.Parse("021e72e4-25fc-4c95-890d-ad3ff074d42f"),
                    AuthorId = Guid.Parse("d2888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Title = "The Winter Wind",
                    Description = "The book that seems impossible to write."
                },
                    new Book()
                    {
                        Id = Guid.Parse("ec175614-738d-4c7b-9188-6f7f0299fae8"),
                        AuthorId = Guid.Parse("fbb5224a-518c-4f09-89d9-a805af0f28f0"),
                        Title = "A Game of thrones",
                        Description = "The first novel in A Song of Ice and Fire."
                    },
                    new Book()
                    {
                        Id = Guid.Parse("ad8dc07b-6573-4e71-af64-97edff5c18ad"),
                        AuthorId = Guid.Parse("895a362c-1f4a-4ba7-8f5f-24f810600dfa"),
                        Title = "Wee Willy Winky",
                        Description = "A story of the night."
                    }
                    );
        }
    }
}
