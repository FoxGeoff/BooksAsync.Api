using BooksAsync.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BooksAsync.Api.Contexts
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author()
                {
                    Id = Guid.Parse("c8077fa3-9c81-4c0d-b5c7-b44884ffaf29"),
                    FirstName = "George",
                    LastName = "RR Martin"
                },
                new Author()
                {
                    Id = Guid.Parse("64d0b6b5-a50c-4e50-9ca5-0851e6eee16d"),
                    FirstName = "Stephen",
                    LastName = "Fry"
                },
                new Author()
                {
                    Id = Guid.Parse("16f33673-09fe-42a8-9e2d-1023a1d65e6f"),
                    FirstName = "James",
                    LastName = "Elroy"
                },
                new Author()
                {
                    Id = Guid.Parse("e731f22a-e272-4ae5-b2eb-290d95770584"),
                    FirstName = "Douglas",
                    LastName = "Adams"
                }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = Guid.Parse("1c714380-05ba-49e3-846a-8dca1f47fbc9"),
                    AuthorId = Guid.Parse("c8077fa3-9c81-4c0d-b5c7-b44884ffaf29"),
                    Title = "The Winds of Winter",
                    Description = "The book that seems imposible to write"
                },
                new Book
                {
                    Id = Guid.Parse("9d3acb2b-7df4-4bd8-9f38-9919f3db1aaf"),
                    AuthorId = Guid.Parse("c8077fa3-9c81-4c0d-b5c7-b44884ffaf29"),
                    Title = "The Summer Sun",
                    Description = "The book that was easy to write"
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
