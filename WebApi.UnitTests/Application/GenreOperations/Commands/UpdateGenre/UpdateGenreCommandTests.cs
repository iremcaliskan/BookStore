using AutoMapper;
using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        //[InlineData(6)] -- passed
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void Theory_WhenNotExistedGenreIdsAreGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            // arrange (preparation) -- hazirlama
            UpdateGenreCommand command = new(_context, _mapper);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel() { Name = "Updated Genre"};

            // act (run) & assert (defend) -- calistirma ve iddia etmek
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No genre found to be updated!");
        }

        [Fact]
        public void Fact_WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            // arrange (preparation) -- hazirlama
            UpdateGenreCommand command = new(_context, _mapper);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel() { Name = "Genre Updated"};

            // act (run) -- calistirma
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert (defend) -- iddia etmek
            var updatedBook = _context.Genres.FirstOrDefault(x => x.Id == command.GenreId);
            updatedBook.Should().NotBeNull();
            updatedBook.Name.Should().Be(command.Model.Name);
        }
    }
}
