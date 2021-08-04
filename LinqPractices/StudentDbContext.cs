using Microsoft.EntityFrameworkCore;

namespace LinqPractices
{
    public class StudentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "StudentDB");
        }

        public DbSet<Student> Students { get; set; }
    }
}