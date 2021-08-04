using Microsoft.EntityFrameworkCore;

namespace EntityViewModelDtoPractices
{
    public class SchoolDbContext : DbContext
    { // Code - Db etkileşimini code tarafında yönettiğimiz yer DbContext'tir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "SchoolDB");
        }

        // SchoolDBContext adında da bir context'imiz olduğunu düşünelim.
        // Student ve Grade sınıflarımızı DbSet<TEntity> formatında context'e göstermemiz gerekiyor.
        // Böylece, Entity Framework DB tarafındaki hangi tabloyu code tarafındaki hangi sınıf ile eşleştireceğini bilecek.
        public DbSet<Student> Students { get; set; } // Student, Students tablosuna denk gelen db objesidir.
        public DbSet<Grade> Grades { get; set; }
    }
}