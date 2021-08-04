using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

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
                if (context.Books.Any()) // Kitap tablosunda veri var mı? - Look for any book
                {
                    return; // Data was already created - Veri varsa üretmeden dön
                }

                // Yoksa aşağıdaki listeyi üret,
                // If any books in book table, create this list of books
                context.Books.AddRange(
                    new Book { Title = "Book 1", GenreId = 1, PageCount = 300, PublishDate = new DateTime(2001, 06, 12) },
                    new Book { Title = "Book 2", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2003, 07, 18) },
                    new Book { Title = "Book 3", GenreId = 3, PageCount = 266, PublishDate = new DateTime(2010, 01, 01) },
                    new Book { Title = "Book 4", GenreId = 3, PageCount = 377, PublishDate = new DateTime(2009, 07, 07) },
                    new Book { Title = "Book 5", GenreId = 2, PageCount = 321, PublishDate = new DateTime(2010, 07, 07) },
                    new Book { Title = "Book 6", GenreId = 3, PageCount = 116, PublishDate = new DateTime(2011, 07, 07) });

                context.SaveChanges();
            }
        }
    }
}