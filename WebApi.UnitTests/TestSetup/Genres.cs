using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context) // extends BookStoreDbContext
        {
            context.Genres.AddRange(
                    new Genre { Name = "Genre 1" },
                    new Genre { Name = "Genre 2" },
                    new Genre { Name = "Genre 3" },
                    new Genre { Name = "Genre 4" },
                    new Genre { Name = "Genre 5" },
                    new Genre { Name = "Genre 6" });
        }
    }
}