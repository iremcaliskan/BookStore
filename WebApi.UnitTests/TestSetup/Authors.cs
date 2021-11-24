using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context) // extends BookStoreDbContext
        {
            context.Authors.AddRange(
                    new Author { FirstName = "A", LastName = "B", Birthdate = new DateTime(1970, 01, 01) },
                    new Author { FirstName = "C", LastName = "D", Birthdate = new DateTime(1960, 12, 07) },
                    new Author { FirstName = "E", LastName = "F", Birthdate = new DateTime(1980, 10, 12) },
                    new Author { FirstName = "G", LastName = "H", Birthdate = new DateTime(1987, 11, 16) });
        }
    }
}