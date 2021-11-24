using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Mappings;

namespace WebApi.UnitTests.TestSetup
{ // AutoMapper + EntityFrameworkCore + InMemory Nuget packages
    public class CommonTestFixture
    { // For Dependencies of Unit Test (Bagimliliklardan Unit Test'i kurtarmak)
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);
            
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            List<Profile> profileList = new() { new BookProfile(), new AuthorProfile(), new GenreProfile() };
            Mapper = new MapperConfiguration(config => config.AddProfiles(profileList)).CreateMapper();
        }
    }
}