﻿using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations
{
    public class BookStoreDbContext : DbContext
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

        // Veritabanı tablosuna karşılık gelen entity eşleştirmesi
        // Matching db table with entity
        public DbSet<Book> Books { get; set; }
    }
}