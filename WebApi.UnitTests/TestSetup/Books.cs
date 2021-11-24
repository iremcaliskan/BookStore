using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context) // extends BookStoreDbContext (extension method for BookStoreDbContext)
        {
            context.Books.AddRange(
                   new Book { Title = "Book 1", PageCount = 300, PublishDate = new DateTime(2001, 06, 12), AuthorId = 1, GenreId = 1 },
                   new Book { Title = "Book 2", PageCount = 250, PublishDate = new DateTime(2003, 07, 18), AuthorId = 2, GenreId = 2 },
                   new Book { Title = "Book 3", PageCount = 266, PublishDate = new DateTime(2010, 01, 01), AuthorId = 3, GenreId = 3 },
                   new Book { Title = "Book 4", PageCount = 377, PublishDate = new DateTime(2009, 07, 07), AuthorId = 4, GenreId = 4 },
                   new Book { Title = "Book 5", PageCount = 321, PublishDate = new DateTime(2010, 07, 07), AuthorId = 1, GenreId = 5 },
                   new Book { Title = "Book 6", PageCount = 116, PublishDate = new DateTime(2011, 07, 07), AuthorId = 2, GenreId = 6 });
        }
    }
}