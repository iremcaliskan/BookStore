using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
        public BookStoreDbContext()
        {

        }

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
            // DbContext'in kullanacağı ayarlar, varsa Context üretim anında kullanılacak özel ayarlar belirtilir
            // Initialize a new instance of Context using the specified options
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Context kullanamı için veritabanı ayarlarını ve diğer ayarların yapıldığı yer
            // Configure the database and other options to be used for this context
            optionsBuilder.UseInMemoryDatabase(databaseName: "BookStoreDB");
            // Geçici işlemler için test etme aşamaları için InMemoryDatabase kullanılır, Normal database'i taklit edemez.
            // Çalışma anında işlem gerçekleştirir. Gerçekte ekleme, silme, güncelleme işlemleri yapmaz.
            // Statik liste ile çalışmanın karşılığıdır.

            // UseInMemoryDatabase does not determine a real database, it's for just testing operations.
        }

        public override int SaveChanges() // DbContext'in otomatik doldurduğu SaveChanges'ı yeniden yazıp aynı işlemi yaptırarak IBookStoreDbContext üzerinden de erişime açılması sağlandı
        {
            return base.SaveChanges();
        }

        // Veritabanı tablosuna karşılık gelen entity eşleştirmesi
        // Matching db table with entity
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
    }
}