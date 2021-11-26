using FluentAssertions;
using System;
using System.Linq;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture> // Provides to access Context
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        //[InlineData(6)] -- passed
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void Theory_WhenNotExistedIdIsGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            // arrange (preparation) -- hazirlama
            DeleteGenreCommand command = new(_context);
            command.GenreId = genreId;

            // act (run) & assert (defend) -- calistirma ve iddia etme
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No genre found to be deleted!");
        }

        [Fact]
        public void Fact_WhenValidIdIsGiven_Book_ShouldBeDeleted()
        {
            // arrange (preparation) -- hazirlama
            DeleteGenreCommand command = new(_context);
            command.GenreId = 1;

            // act (run) -- calistirma
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert (defend) -- iddia etmek
            var deletedGenre = _context.Genres.FirstOrDefault(x => x.Id == command.GenreId);
            deletedGenre.Should().BeNull();
        }
    }
}