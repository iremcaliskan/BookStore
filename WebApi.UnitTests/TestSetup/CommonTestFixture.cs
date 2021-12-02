using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using WebApi.DbOperations;
using WebApi.Mappings;

namespace WebApi.UnitTests.TestSetup
{ // AutoMapper + EntityFrameworkCore + InMemory Nuget packages
    public class CommonTestFixture
    { // For Dependencies of Unit Test (Bagimliliklardan Unit Test'i kurtarmak)
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);
            
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddUsers();
            Context.SaveChanges();

            List<Profile> profileList = new() { new BookProfile(), new AuthorProfile(), new GenreProfile(), new UserProfile() };
            Mapper = new MapperConfiguration(config => config.AddProfiles(profileList)).CreateMapper();

            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}