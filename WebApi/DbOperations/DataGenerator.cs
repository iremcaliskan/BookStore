using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    { // For InMemoryDatabase
        public static void Initialize(IServiceProvider serviceProvider)
        { // Çalışma anında oluşturulacak veriler için yapılandırma
            // Configuration for datas created at runtime
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            { // Belirli özellikler dahilinde üretilecek olan Context'in yapılandırılması
              // Initialize a new instance of Context using specified options like here

                if (context.Genres.Any()) // Look for any genre
                {
                    return; // Data was already created
                }

                // If any genres in genre table, create this list of genres
                context.Genres.AddRange(
                    new Genre { Name = "Genre 1" },
                    new Genre { Name = "Genre 2" },
                    new Genre { Name = "Genre 3" },
                    new Genre { Name = "Genre 4" },
                    new Genre { Name = "Genre 5" },
                    new Genre { Name = "Genre 6" });

                if (context.Authors.Any()) // Look for any author
                {
                    return; // Data was already created
                }

                // If any author in author table, create this list of authors
                context.Authors.AddRange(
                    new Author { FirstName = "A", LastName = "B", Birthdate = new DateTime(1970, 01, 01) },
                    new Author { FirstName = "C", LastName = "D", Birthdate = new DateTime(1960, 12, 07) },
                    new Author { FirstName = "E", LastName = "F", Birthdate = new DateTime(1980, 10, 12) },
                    new Author { FirstName = "G", LastName = "H", Birthdate = new DateTime(1987, 11, 16) });

                if (context.Books.Any()) // Kitap tablosunda veri var mı? - Look for any book
                {
                    return; // Data was already created - Veri varsa üretmeden dön
                }

                // Yoksa aşağıdaki listeyi üret,
                // If any books in book table, create this list of books
                context.Books.AddRange(
                    new Book { Title = "Book 1", PageCount = 300, PublishDate = new DateTime(2001, 06, 12), AuthorId = 1, GenreId = 1 },
                    new Book { Title = "Book 2", PageCount = 250, PublishDate = new DateTime(2003, 07, 18), AuthorId = 2, GenreId = 2 },
                    new Book { Title = "Book 3", PageCount = 266, PublishDate = new DateTime(2010, 01, 01), AuthorId = 3, GenreId = 3 },
                    new Book { Title = "Book 4", PageCount = 377, PublishDate = new DateTime(2009, 07, 07), AuthorId = 4, GenreId = 4 },
                    new Book { Title = "Book 5", PageCount = 321, PublishDate = new DateTime(2010, 07, 07), AuthorId = 1, GenreId = 5 },
                    new Book { Title = "Book 6", PageCount = 116, PublishDate = new DateTime(2011, 07, 07), AuthorId = 2, GenreId = 6 });
                context.SaveChanges();
            }
        }
    }
}