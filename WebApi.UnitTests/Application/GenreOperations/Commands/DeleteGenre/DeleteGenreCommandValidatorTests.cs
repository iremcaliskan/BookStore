using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests
    {
        [Fact]
        public void Fact_WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange (preparation) -- hazirlama
            DeleteGenreCommand command = new(null); // Delete process is not important in here
            command.GenreId = 0;

            // act (run) -- calistirma
            DeleteGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory] // Sample data
        [InlineData(0)]
        [InlineData(-1)]
        public void Theory_WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange (preparation) -- hazirlama
            DeleteGenreCommand command = new(null); // Delete process is not important in here
            command.GenreId = genreId;

            // act (run) -- calistirma
            DeleteGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // Happy Path
        public void WhenValidInputIsGiven_Validator_ShouldBeReturnSuccess()
        {
            // arrange (preparation) -- hazirlama
            DeleteGenreCommand command = new(null);
            command.GenreId = 1;

            // act (run) -- calistirma
            DeleteGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().Be(0);
        }

        [Theory] // Sample data + Happy Path
        [InlineData(1)]
        [InlineData(16)]
        [InlineData(66)]
        [InlineData(100)]
        public void Theory_WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess(int genreId)
        {
            // arrange (preparation) -- hazirlama
            DeleteGenreCommand command = new(null); // Delete process is not important in here
            command.GenreId = genreId;

            // act (run) -- calistirma
            DeleteGenreCommandValidator validator = new();
            var validationResult = validator.Validate(command);

            // assert (defend) -- iddia etmek
            validationResult.Errors.Count.Should().Be(0);
        }
    }
}