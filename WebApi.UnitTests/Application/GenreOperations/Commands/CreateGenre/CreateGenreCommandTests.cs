using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture> // Provides to access Context and Mapper
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact] // Fact attribute define us that the method is a test method, Fact works for test data running in one condition
        public void Fact_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn() // Parameter-less
        {
            // arrange (preparation) -- hazirlama
            var genre = new Genre() { Name = "Genre"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new(_context, _mapper);
            command.Model = new CreateGenreModel() { Name = genre.Name};

            // act (run) & assert (defend) -- calistirma ve iddia etmek
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre is already in the system.");
        }

        [Fact] // Happy Path
        public void Fact_WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // arrange (preparation) -- hazirlama
            CreateGenreCommand command = new(_context, _mapper);
            command.Model = new CreateGenreModel() { Name = "Genre"};

            // act (run) -- calistirma
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert (defend) -- iddia etmek
            var addedGenre = _context.Genres.FirstOrDefault(x => x.Name == command.Model.Name);
            addedGenre.Should().NotBeNull();
            addedGenre.Name.Should().Be(command.Model.Name);
        }

        [Theory]
        //[InlineData(null)]
        [InlineData("")]
        [InlineData("n")]
        [InlineData("na")]
        [InlineData("nam")]
        [InlineData("name")] // Min Length is 4
        [InlineData("name1")]
        public void Theory_WhenValidNamesAreGiven_Genre_ShouldBeCreated(string name)
        {
            // arrange (preparation) -- hazirlama
            CreateGenreCommand command = new(_context, _mapper);
            command.Model = new CreateGenreModel() { Name = name };

            // act (run) -- calistirma
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert (defend) -- iddia etmek
            var addedGenre = _context.Genres.FirstOrDefault(x => x.Name == command.Model.Name);
            addedGenre.Should().NotBeNull();
            addedGenre.Name.Should().Be(command.Model.Name);
        }
    }
}